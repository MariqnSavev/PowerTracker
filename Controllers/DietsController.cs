using Microsoft.AspNetCore.Authorization; // Добавяме using за атрибута [Authorize]
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoxingAppDiploma.Data;
using BoxingAppDiploma.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingAppDiploma.Controllers
{
    [Authorize] // Добавено тук, за да защити всички действия в контролера
    public class DietsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DietsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Diets
        public IActionResult Index()
        {
            var diets = _context.Diet.ToList();
            return View(diets);
        }

        // GET: Diets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Diets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Diet diet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diet);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(diet);
        }

        // GET: Diets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diet = await _context.Diet.FindAsync(id);
            if (diet == null)
            {
                return NotFound();
            }
            return View(diet);
        }

        // POST: Diets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,MealDescription,Calories")] Diet diet)
        {
            if (id != diet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DietExists(diet.Id))
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
            return View(diet);
        }

        // GET: Diets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diet = await _context.Diet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diet == null)
            {
                return NotFound();
            }

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

        // GET: Diets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Ако не е подаден ID, връща грешка
            }

            var diet = await _context.Diet
                .FirstOrDefaultAsync(m => m.Id == id); // Търсим диетата по ID

            if (diet == null)
            {
                return NotFound(); // Ако не намерим диетата с това ID
            }

            return View(diet); // Връщаме изгледа с намерената диета
        }

        private bool DietExists(int id)
        {
            return _context.Diet.Any(e => e.Id == id);
        }
    }
}