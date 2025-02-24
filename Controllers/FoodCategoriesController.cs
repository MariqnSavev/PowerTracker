using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerTracker.Data;
using PowerTracker.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PowerTracker.Controllers
{
    public class FoodCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FoodCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FoodCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.FoodCategories.ToListAsync());
        }

        // GET: FoodCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var category = await _context.FoodCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null) return NotFound();

            return View(category);
        }

        // GET: FoodCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FoodCategories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] FoodCategories category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: FoodCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var category = await _context.FoodCategories.FindAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }

        // POST: FoodCategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] FoodCategories category)
        {
            if (id != category.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.FoodCategories.Any(e => e.Id == category.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: FoodCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var category = await _context.FoodCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null) return NotFound();

            return View(category);
        }

        // POST: FoodCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.FoodCategories.FindAsync(id);
            if (category != null)
            {
                _context.FoodCategories.Remove(category);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
