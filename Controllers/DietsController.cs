using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // 📌 GET: Diets (списък на диетите)
        public async Task<IActionResult> Index()
        {
            var diets = _context.Diet
                .Include(d => d.Food)
                .Include(d => d.Category);
            return View(await diets.ToListAsync());
        }

        // 📌 GET: Diets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var diet = await _context.Diet
                .Include(d => d.Food)
                .Include(d => d.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diet == null) return NotFound();

            return View(diet);
        }

        // 📌 GET: Diets/Create
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.FoodCategories, "Id", "Name");
            ViewBag.Foods = new SelectList(new List<Foods>(), "Id", "Name"); // Празно меню за храни
            return View();
        }

        // 📌 POST: Diets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,FoodId,QuantityInGrams")] Diet diet)
        {
            ModelState.Remove("Category");
            ModelState.Remove("Food");

            if (ModelState.IsValid)
            {
                var food = await _context.Foods.FindAsync(diet.FoodId);
                if (food != null)
                {
                    diet.Calories = (diet.QuantityInGrams / 100) * food.CaloriesPer100g;
                }

                _context.Add(diet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(_context.FoodCategories, "Id", "Name", diet.CategoryId);
            ViewBag.Foods = new SelectList(_context.Foods.Where(f => f.CategoryId == diet.CategoryId), "Id", "Name", diet.FoodId);
            return View(diet);
        }

        // 📌 GET: Diets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var diet = await _context.Diet.FindAsync(id);
            if (diet == null) return NotFound();

            ViewBag.Categories = new SelectList(_context.FoodCategories, "Id", "Name", diet.CategoryId);
            ViewBag.Foods = new SelectList(_context.Foods.Where(f => f.CategoryId == diet.CategoryId), "Id", "Name", diet.FoodId);
            return View(diet);
        }

        // 📌 POST: Diets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,FoodId,QuantityInGrams,Calories,Date")] Diet diet)
        {
            if (id != diet.Id) return NotFound();

            ModelState.Remove("Category");
            ModelState.Remove("Food");

            if (ModelState.IsValid)
            {
                try
                {
                    var food = await _context.Foods.FindAsync(diet.FoodId);
                    if (food != null)
                    {
                        diet.Calories = (diet.QuantityInGrams / 100) * food.CaloriesPer100g;
                    }

                    _context.Update(diet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Diet.Any(e => e.Id == diet.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(_context.FoodCategories, "Id", "Name", diet.CategoryId);
            ViewBag.Foods = new SelectList(_context.Foods.Where(f => f.CategoryId == diet.CategoryId), "Id", "Name", diet.FoodId);
            return View(diet);
        }

        // 📌 GET: Diets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var diet = await _context.Diet
                .Include(d => d.Food)
                .Include(d => d.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diet == null) return NotFound();

            return View(diet);
        }

        // 📌 POST: Diets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diet = await _context.Diet.FindAsync(id);
            if (diet != null)
            {
                _context.Diet.Remove(diet);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // 📌 AJAX: Зареждане на храни по избрана категория
        [HttpGet]
        public JsonResult GetFoodsByCategory(int categoryId)
        {
            var foods = _context.Foods
                .Where(f => f.CategoryId == categoryId)
                .Select(f => new { f.Id, f.Name })
                .ToList();
            return Json(foods);
        }
    }
}
