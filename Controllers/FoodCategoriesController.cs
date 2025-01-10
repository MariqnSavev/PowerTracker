using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PowerTracker.Data;
using PowerTracker.Models;

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
              return _context.FoodCategories != null ? 
                          View(await _context.FoodCategories.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.FoodCategories'  is null.");
        }

        // GET: FoodCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FoodCategories == null)
            {
                return NotFound();
            }

            var foodCategories = await _context.FoodCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodCategories == null)
            {
                return NotFound();
            }

            return View(foodCategories);
        }

        // GET: FoodCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FoodCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameOfCategorie")] FoodCategories foodCategories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(foodCategories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(foodCategories);
        }

        // GET: FoodCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FoodCategories == null)
            {
                return NotFound();
            }

            var foodCategories = await _context.FoodCategories.FindAsync(id);
            if (foodCategories == null)
            {
                return NotFound();
            }
            return View(foodCategories);
        }

        // POST: FoodCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameOfCategorie")] FoodCategories foodCategories)
        {
            if (id != foodCategories.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodCategories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodCategoriesExists(foodCategories.Id))
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
            return View(foodCategories);
        }

        // GET: FoodCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FoodCategories == null)
            {
                return NotFound();
            }

            var foodCategories = await _context.FoodCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodCategories == null)
            {
                return NotFound();
            }

            return View(foodCategories);
        }

        // POST: FoodCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FoodCategories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FoodCategories'  is null.");
            }
            var foodCategories = await _context.FoodCategories.FindAsync(id);
            if (foodCategories != null)
            {
                _context.FoodCategories.Remove(foodCategories);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodCategoriesExists(int id)
        {
          return (_context.FoodCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
