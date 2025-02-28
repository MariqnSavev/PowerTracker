using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerTracker.Data;
using PowerTracker.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PowerTracker.Controllers
{
    public class GoalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GoalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 📌 GET: Goals (Всички цели)
        public async Task<IActionResult> Index()
        {
            return View(await _context.Goal.ToListAsync());
        }

        // 📌 GET: Goals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var goal = await _context.Goal.FirstOrDefaultAsync(m => m.Id == id);
            if (goal == null) return NotFound();

            return View(goal);
        }

        // 📌 GET: Goals/Create
        public IActionResult Create()
        {
            return View();
        }

        // 📌 POST: Goals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,StartWeight,TargetWeight,StartDate,EndDate")] Goal goal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(goal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(goal);
        }

        // 📌 GET: Goals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var goal = await _context.Goal.FindAsync(id);
            if (goal == null) return NotFound();

            return View(goal);
        }

        // 📌 POST: Goals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartWeight,TargetWeight,StartDate,EndDate")] Goal goal)
        {
            if (id != goal.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(goal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Goal.Any(e => e.Id == goal.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(goal);
        }

        // 📌 GET: Goals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var goal = await _context.Goal.FirstOrDefaultAsync(m => m.Id == id);
            if (goal == null) return NotFound();

            return View(goal);
        }

        // 📌 POST: Goals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var goal = await _context.Goal.FindAsync(id);
            if (goal != null)
            {
                _context.Goal.Remove(goal);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
