using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerTracker.Data;
using PowerTracker.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PowerTracker.Controllers
{
    [Authorize]
    public class GoalsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public GoalsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // 📌 GET: Всички цели на текущия потребител
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // ID на текущия потребител

            var goals = _context.Goals
                .Where(g => g.UserId == userId) // 🚀 Филтрираме само собствените записи
                .AsNoTracking();

            return View(await goals.ToListAsync());
        }

        // 📌 GET: Детайли за цел (само ако тя принадлежи на текущия потребител)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var goal = await _context.Goals
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId); // 🚀 Проверяваме дали целта принадлежи на текущия потребител

            if (goal == null) return NotFound();

            return View(goal);
        }

        // 📌 GET: Създаване на нова цел
        public IActionResult Create()
        {
            return View();
        }

        // 📌 POST: Създаване на цел (автоматично добавяне на `UserId`)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,StartWeight,TargetWeight,StartDate,EndDate")] Goal goal)
        {
            if (ModelState.IsValid)
            {
                goal.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // 🚀 Автоматично свързване на целта с потребителя

                _context.Add(goal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(goal);
        }

        // 📌 GET: Редактиране на цел (само за собствени цели)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var goal = await _context.Goals.FirstOrDefaultAsync(g => g.Id == id && g.UserId == userId); // 🚀 Проверяваме дали потребителят притежава тази цел
            if (goal == null) return NotFound();

            return View(goal);
        }

        // 📌 POST: Редактиране на цел (само за собствени записи)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartWeight,TargetWeight,StartDate,EndDate")] Goal goal)
        {
            if (id != goal.Id) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Ако целта не принадлежи на потребителя, връщаме Unauthorized
            if (goal.UserId != userId) return Unauthorized();

            if (ModelState.IsValid)
            {
                try
                {
                    // Премахваме UserId от полето за актуализация, защото не трябва да се променя
                    goal.UserId = userId;

                    _context.Update(goal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Goals.Any(e => e.Id == goal.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(goal);
        }

        // 📌 GET: Изтриване на цел (само за собствени цели)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var goal = await _context.Goals
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId); // 🚀 Само собствените записи

            if (goal == null) return NotFound();

            return View(goal);
        }

        // 📌 POST: Изтриване на цел (само ако записът принадлежи на текущия потребител)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var goal = await _context.Goals.FirstOrDefaultAsync(g => g.Id == id && g.UserId == userId); // 🚀 Само собствените записи
            if (goal != null)
            {
                _context.Goals.Remove(goal);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}



















































































































