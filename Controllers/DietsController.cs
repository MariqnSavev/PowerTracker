using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PowerTracker.Data;
using PowerTracker.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PowerTracker.Controllers
{
    [Authorize] // Само влезли потребители могат да управляват храненията си
    public class DietsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DietsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // 📌 GET: Всички хранения на текущия потребител
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var diets = await _context.Diets
                .Where(d => d.UserId == userId)
                .Include(d => d.Food)
                .Include(d => d.Category)
                .ToListAsync();

            return View(diets);
        }

        // 📌 GET: Детайли за хранене
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var diet = await _context.Diets
                .Include(d => d.Food)
                .Include(d => d.Category)
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (diet == null) return NotFound();

            return View(diet);
        }

        // 📌 GET: Създаване на хранене
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.FoodCategories, "Id", "Name");
            ViewBag.Foods = new SelectList(new List<Foods>(), "Id", "Name");
            return View();
        }

        // 📌 POST: Създаване на хранене
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,FoodId,QuantityInGrams")] Diet diet)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId)) return Unauthorized(); // 🛑 Ако няма влязъл потребител

                diet.UserId = userId; // 🚀 Задаване на текущия потребител

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


        // 📌 GET: Редактиране на хранене
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var diet = await _context.Diets.FirstOrDefaultAsync(d => d.Id == id && d.UserId == userId);
            if (diet == null) return NotFound();

            ViewBag.Categories = new SelectList(_context.FoodCategories, "Id", "Name", diet.CategoryId);
            ViewBag.Foods = new SelectList(_context.Foods.Where(f => f.CategoryId == diet.CategoryId), "Id", "Name", diet.FoodId);
            return View(diet);
        }

        // 📌 POST: Редактиране на хранене
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,FoodId,QuantityInGrams,Calories,Date,UserId")] Diet diet)
        {
            if (id != diet.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (diet.UserId != userId) return Unauthorized();

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
                    if (!_context.Diets.Any(e => e.Id == diet.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(_context.FoodCategories, "Id", "Name", diet.CategoryId);
            ViewBag.Foods = new SelectList(_context.Foods.Where(f => f.CategoryId == diet.CategoryId), "Id", "Name", diet.FoodId);
            return View(diet);
        }

        // 📌 GET: Изтриване на хранене
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var diet = await _context.Diets
                .Include(d => d.Food)
                .Include(d => d.Category)
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (diet == null) return NotFound();

            return View(diet);
        }

        // 📌 POST: Изтриване на хранене
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var diet = await _context.Diets.FirstOrDefaultAsync(d => d.Id == id && d.UserId == userId);
            if (diet != null)
            {
                _context.Diets.Remove(diet);
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
