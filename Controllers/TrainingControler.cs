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
    [Authorize]
    public class TrainingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TrainingsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Helper method to calculate calories burned
        private double CalculateCaloriesBurned(string activity, int durationMinutes, double weight)
        {
            var metValues = new Dictionary<string, double>
            {
                { "Running", 9.8 },
                { "Cycling", 7.5 },
                { "Walking", 3.8 },
                { "Weightlifting", 6.0 }
            };

            return metValues.ContainsKey(activity)
                ? (metValues[activity] * weight * durationMinutes) / 60.0
                : 0.0;
        }

        // GET: Trainings
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trainings = await _context.Trainings
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.Date)
                .AsNoTracking()
                .ToListAsync();

            return View(trainings);
        }

        // GET: Trainings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var training = await _context.Trainings
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            return training == null ? NotFound() : View(training);
        }

        // GET: Trainings/Create
        public IActionResult Create()
        {
            return View(new Training
            {
                Date = DateTime.Now,
                WeightInKg = 70 // Default weight, can be changed
            });
        }

        // POST: Trainings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,Description,Activity,DurationMinutes,WeightInKg")] Training training)
        {
            if (ModelState.IsValid)
            {
                training.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                training.CaloriesBurned = CalculateCaloriesBurned(
                    training.Activity,
                    training.DurationMinutes,
                    training.WeightInKg);

                _context.Add(training);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(training);
        }

        // GET: Trainings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var training = await _context.Trainings.FindAsync(id);

            if (training == null || training.UserId != userId)
                return NotFound();

            return View(training);
        }

        // POST: Trainings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Date,Description,Activity,DurationMinutes,WeightInKg")] Training training)
        {
            if (id != training.Id) return NotFound();

            // Verify ownership
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (training.UserId != userId) return Unauthorized();

            if (ModelState.IsValid)
            {
                try
                {
                    // Recalculate calories
                    training.CaloriesBurned = CalculateCaloriesBurned(
                        training.Activity,
                        training.DurationMinutes,
                        training.WeightInKg);

                    _context.Update(training);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingExists(training.Id))
                        return NotFound();
                    throw;
                }
            }
            return View(training);
        }

        // GET: Trainings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var training = await _context.Trainings
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            return training == null ? NotFound() : View(training);
        }

        // POST: Trainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var training = await _context.Trainings
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (training != null)
            {
                _context.Trainings.Remove(training);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingExists(int id)
        {
            return _context.Trainings.Any(e => e.Id == id);
        }

        // AJAX endpoint to calculate calories (optional)
        [HttpGet]
        public IActionResult CalculateCalories(string activity, int duration, double weight)
        {
            var calories = CalculateCaloriesBurned(activity, duration, weight);
            return Json(new { calories });
        }
    }
}