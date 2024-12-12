using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        public TrainingsController(ApplicationDbContext context)
        {
            _context = context;
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

        // GET: Trainings
        public async Task<IActionResult> Index()
        {
            return _context.Training != null ?
                       View(await _context.Training.ToListAsync()) :
                       Problem("Entity set 'ApplicationDbContext.Training' is null.");
        }

        // GET: Trainings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Training == null)
            {
                return NotFound();
            }

            var training = await _context.Training.FirstOrDefaultAsync(m => m.Id == id);
            if (training == null)
            {
                return NotFound();
            }

            return View(training);
        }

        // GET: Trainings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trainings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Description,Activity,DurationMinutes,WeightInKg")] Training training)
        {
            if (ModelState.IsValid)
            {
                // Изчисляване на калориите
                training.CaloriesBurned = CalculateCaloriesBurned(training.Activity, training.DurationMinutes, training.WeightInKg);

                // Записване в базата данни
                _context.Add(training);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(training);
        }

        // GET: Trainings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Training == null)
            {
                return NotFound();
            }

            var training = await _context.Training.FindAsync(id);
            if (training == null)
            {
                return NotFound();
            }
            return View(training);
        }

        // POST: Trainings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Description,Activity,DurationMinutes,WeightInKg")] Training training)
        {
            if (id != training.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Изчисляване на калориите
                    training.CaloriesBurned = CalculateCaloriesBurned(training.Activity, training.DurationMinutes, training.WeightInKg);

                    // Обновяване на записа
                    _context.Update(training);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingExists(training.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(training);
        }

        // GET: Trainings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Training == null)
            {
                return NotFound();
            }

            var training = await _context.Training.FirstOrDefaultAsync(m => m.Id == id);
            if (training == null)
            {
                return NotFound();
            }

            return View(training);
        }

        // POST: Trainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Training == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Training' is null.");
            }
            var training = await _context.Training.FindAsync(id);
            if (training != null)
            {
                _context.Training.Remove(training);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingExists(int id)
        {
            return (_context.Training?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
