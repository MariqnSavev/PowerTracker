using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerTracker.Data;
using PowerTracker.Models;
using System.Security.Claims;

namespace PowerTracker.Controllers
{
    [Authorize]
    public class DietsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DietsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: All diets for current user
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var diets = await _context.Diets
                .Where(d => d.UserId == userId)
                .Include(d => d.Food)
                .ThenInclude(f => f.Category)
                .OrderByDescending(d => d.Date)
                .ToListAsync();

            return View(diets);
        }

        // GET: Diet details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var diet = await _context.Diets
                .Include(d => d.Food)
                .ThenInclude(f => f.Category)
                .FirstOrDefaultAsync(d => d.Id == id && d.UserId == userId);

            return diet == null ? NotFound() : View(diet);
        }

        // GET: Create diet
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.FoodCategories, "Id", "Name");
            ViewBag.Foods = new SelectList(new List<Foods>(), "Id", "Name");
            return View(new Diet { Date = DateTime.Now });
        }

        // POST: Create diet
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FoodId,QuantityInGrams,Date")] Diet diet, int CategoryId)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                diet.UserId = userId;

                var food = await _context.Foods.FindAsync(diet.FoodId);
                if (food != null)
                {
                    diet.Calories = (diet.QuantityInGrams / 100) * food.CaloriesPer100g;
                }

                _context.Add(diet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(_context.FoodCategories, "Id", "Name", CategoryId);
            ViewBag.Foods = new SelectList(_context.Foods.Where(f => f.CategoryId == CategoryId), "Id", "Name", diet.FoodId);
            return View(diet);
        }

        // GET: Edit diet
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var diet = await _context.Diets
                .Include(d => d.Food)
                .FirstOrDefaultAsync(d => d.Id == id && d.UserId == userId);

            if (diet == null) return NotFound();

            var categoryId = diet.Food?.CategoryId ?? 0;

            ViewBag.Categories = new SelectList(_context.FoodCategories, "Id", "Name", categoryId);
            ViewBag.Foods = new SelectList(
                _context.Foods.Where(f => f.CategoryId == categoryId),
                "Id", "Name", diet.FoodId);

            return View(diet);
        }

        // POST: Edit diet
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FoodId,QuantityInGrams,Date,UserId")] Diet diet, int CategoryId)
        {
            if (id != diet.Id) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (diet.UserId != userId) return Unauthorized();

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
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DietExists(diet.Id)) return NotFound();
                    throw;
                }
            }

            ViewBag.Categories = new SelectList(_context.FoodCategories, "Id", "Name", CategoryId);
            ViewBag.Foods = new SelectList(_context.Foods.Where(f => f.CategoryId == CategoryId), "Id", "Name", diet.FoodId);
            return View(diet);
        }

        // GET: Delete diet
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var diet = await _context.Diets
                .Include(d => d.Food)
                .ThenInclude(f => f.Category)
                .FirstOrDefaultAsync(d => d.Id == id && d.UserId == userId);

            return diet == null ? NotFound() : View(diet);
        }

        // POST: Delete diet
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var diet = await _context.Diets.FirstOrDefaultAsync(d => d.Id == id && d.UserId == userId);

            if (diet == null) return NotFound();

            _context.Diets.Remove(diet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // AJAX: Get foods by category
        [HttpGet]
        public JsonResult GetFoodsByCategory(int categoryId)
        {
            var foods = _context.Foods
                .Where(f => f.CategoryId == categoryId)
                .Select(f => new { id = f.Id, name = f.Name })
                .ToList();
            return Json(foods);
        }

        private bool DietExists(int id)
        {
            return _context.Diets.Any(e => e.Id == id);
        }
    }
}