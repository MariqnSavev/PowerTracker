using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerTracker.Data;
using PowerTracker.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PowerTracker.Controllers
{
    public class DietsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DietsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Diets
        public async Task<IActionResult> Index()
        {
            var diets = await _context.Diet
                .Include(d => d.Food)
                .ThenInclude(f => f.Category)  // Включваме и категорията
                .ToListAsync();
            return View(diets);
        }

        // GET: Diets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var diet = await _context.Diet
                .Include(d => d.Food)
                .ThenInclude(f => f.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (diet == null) return NotFound();
            return View(diet);
        }

        // GET: Diet/Create
        public IActionResult Create()
        {
            ViewBag.Categories = _context.FoodCategories.ToList();
            ViewBag.Foods = _context.Foods.ToList();
            return View();
        }

        // API: Get Foods by Category
        [HttpGet]
        public async Task<JsonResult> GetFoodsByCategory(int categoryId)
        {
            var foods = await _context.Foods
                .Where(f => f.CategoryId == categoryId)
                .Select(f => new { f.Id, f.Name })
                .ToListAsync();

            return Json(foods);
        }

        // POST: Diet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FoodId,CaloriesPer100g,QuantityInGrams,Date")] Diet diet)
        {
            if (ModelState.IsValid)
            {
                diet.CalculateCalories();
                _context.Add(diet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _context.FoodCategories.ToList();
            ViewBag.Foods = _context.Foods.ToList();
            return View(diet);
        }

        // GET: Diets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var diet = await _context.Diet.FindAsync(id);
            if (diet == null) return NotFound();

            ViewBag.Categories = _context.FoodCategories.ToList();
            ViewBag.Foods = _context.Foods.Where(f => f.CategoryId == diet.Food.CategoryId).ToList();
            return View(diet);
        }

        // POST: Diets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FoodId,CaloriesPer100g,QuantityInGrams,Calories,Date")] Diet diet)
        {
            if (id != diet.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    diet.CalculateCalories();
                    _context.Update(diet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Diet.Any(e => e.Id == diet.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _context.FoodCategories.ToList();
            ViewBag.Foods = _context.Foods.Where(f => f.CategoryId == diet.Food.CategoryId).ToList();
            return View(diet);
        }

        // GET: Diets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var diet = await _context.Diet
                .Include(d => d.Food)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diet == null) return NotFound();

            return View(diet);
        }

        // POST: Diets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diet = await _context.Diet.FindAsync(id);
            _context.Diet.Remove(diet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
