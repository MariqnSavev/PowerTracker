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

        // 📌 GET: Diets
        public async Task<IActionResult> Index()
        {
            var diets = _context.Diet.Include(d => d.Food).Include(d => d.Category);
            return View(await diets.ToListAsync());
        }

        // 📌 GET: Diets/Create
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.FoodCategories, "Id", "Name");
            return View();
        }

        // 📌 POST: Diets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,FoodId,QuantityInGrams,Date")] Diet diet)
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

        // 📌 AJAX: Зареждане на храни по категория
        [HttpGet]
        public JsonResult GetFoodsByCategory(int categoryId)
        {
            var foods = _context.Foods
                .Where(f => f.CategoryId == categoryId)
                .Select(f => new { f.Id, f.Name })
                .ToList();

            Console.WriteLine($"Търсим храни за категория: {categoryId}");
            Console.WriteLine($"Намерени: {foods.Count}");

            return Json(foods);
        }
    }
}
