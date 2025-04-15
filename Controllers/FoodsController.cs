using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerTracker.Data;
using PowerTracker.Models;
using PowerTracker.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PowerTracker.Controllers
{
    public class FoodsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INutritionixService _nutritionixService;

        public FoodsController(ApplicationDbContext context, INutritionixService nutritionixService)
        {
            _context = context;
            _nutritionixService = nutritionixService;
        }

        // GET: Foods
        public IActionResult Index()
        {
            var foods = _context.Food.OrderByDescending(f => f.DateAdded).ToList();

            ViewData["TotalCalories"] = foods.Sum(f => f.Calories);
            ViewData["TotalProtein"] = foods.Sum(f => f.Protein);
            ViewData["TotalCarbs"] = foods.Sum(f => f.Carbs);
            ViewData["TotalFat"] = foods.Sum(f => f.Fat);

            return View(foods);
        }

        // GET: Foods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Foods/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Food food)
        {
            if (ModelState.IsValid)
            {
                food.DateAdded = DateTime.Now;
                _context.Add(food);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(food);
        }

        // GET: Foods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var food = await _context.Food.FindAsync(id);
            if (food == null) return NotFound();

            return View(food);
        }

        // POST: Foods/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Food food)
        {
            if (id != food.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(food);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Food.Any(e => e.Id == food.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(food);
        }

        // GET: Foods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var food = await _context.Food.FirstOrDefaultAsync(m => m.Id == id);
            if (food == null) return NotFound();

            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var food = await _context.Food.FindAsync(id);
            if (food != null)
            {
                _context.Food.Remove(food);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Foods/Search
        [HttpPost]
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                TempData["Error"] = "Моля въведи име на храна.";
                return RedirectToAction(nameof(Index));
            }

            var result = await _nutritionixService.SearchFoodAsync(query);

            if (result == null)
            {
                TempData["Error"] = "Храна не е намерена.";
                return RedirectToAction(nameof(Index));
            }

            var food = new Food
            {
                Name = result.FoodName,
                Calories = result.Calories,
                Protein = result.Protein,
                Fat = result.TotalFat,
                Carbs = result.TotalCarbohydrate,
                Brand = result.BrandName,
                ServingSize = result.ServingWeightGrams,
                PhotoUrl = result.Photo,
                DateAdded = DateTime.Now
            };

            _context.Food.Add(food);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
