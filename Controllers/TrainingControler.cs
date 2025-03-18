using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerTracker.Data;
using PowerTracker.Models;

namespace PowerTracker.Controllers
{
    [Authorize(Roles = "User")]
    public class TrainingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TrainingsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Метод за изчисление на калориите
        private double CalculateCaloriesBurned(string activity, int durationMinutes, double weight)
        {
            // MET стойности за различни тренировки
            var metValues = new Dictionary<string, double>
            {
                { "Running", 9.8 },
                { "Cycling", 7.5 },
                { "Walking", 3.8 },
                { "Weightlifting", 6.0 }
            };

            if (metValues.ContainsKey(activity))
            {
                double met = metValues[activity];
                return (met * weight * durationMinutes) / 60.0; // Формула за изчисление на калории
            }

            return 0.0; // Ако видът на тренировката не е намерен
        }

        // 📌 GET: Trainings (само тренировките на текущия потребител)
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // ID на текущия потребител

            var trainings = _context.Trainings
                .Where(t => t.UserId == userId) // 🚀 Филтрираме само собствените записи
                .AsNoTracking();

            return View(await trainings.ToListAsync());
        }

        // 📌 GET: Trainings/Details/5 (само ако тя принадлежи на текущия потребител)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var training = await _context.Trainings
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId); // 🚀 Проверяваме дали тренировката принадлежи на текущия потребител

            if (training == null) return NotFound();

            return View(training);
        }

        // 📌 GET: Trainings/Create
        public IActionResult Create()
        {
            return View();
        }

        // 📌 POST: Trainings/Create (автоматично добавяне на `UserId`)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,Description,Activity,DurationMinutes,WeightInKg")] Training training)
        {
            if (ModelState.IsValid)
            {
                training.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // 🚀 Автоматично свързване на тренировката с потребителя
                training.CaloriesBurned = CalculateCaloriesBurned(training.Activity, training.DurationMinutes, training.WeightInKg);

                _context.Add(training);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(training);
        }

        // 📌 GET: Trainings/Edit/5 (само за собствени тренировки)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var training = await _context.Trainings.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId); // 🚀 Проверяваме дали потребителят притежава тази тренировка
            if (training == null) return NotFound();

            return View(training);
        }

        // 📌 POST: Trainings/Edit/5 (само за собствени записи)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Description,Activity,DurationMinutes,WeightInKg,UserId")] Training training)
        {
            if (id != training.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (training.UserId != userId) return Unauthorized(); // 🚀 Защита: Потребителите не могат да редактират чужди тренировки

                try
                {
                    training.CaloriesBurned = CalculateCaloriesBurned(training.Activity, training.DurationMinutes, training.WeightInKg);
                    _context.Update(training);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Trainings.Any(e => e.Id == training.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(training);
        }

        // 📌 GET: Trainings/Delete/5 (само за собствени тренировки)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var training = await _context.Trainings
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId); // 🚀 Само собствените записи

            if (training == null) return NotFound();

            return View(training);
        }

        // 📌 POST: Trainings/Delete/5 (само ако записът принадлежи на текущия потребител)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var training = await _context.Trainings.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId); // 🚀 Само собствените записи
            if (training != null)
            {
                _context.Trainings.Remove(training);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
