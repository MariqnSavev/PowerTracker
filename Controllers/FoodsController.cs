﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PowerTracker.Data;
using PowerTracker.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PowerTracker.Controllers
{
    public class FoodsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FoodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 📌 GET: Foods (Списък с храни)
        public async Task<IActionResult> Index()
        {
            var foods = _context.Foods.Include(f => f.Category);
            return View(await foods.ToListAsync());
        }

        // 📌 GET: Foods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var food = await _context.Foods
                .Include(f => f.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (food == null) return NotFound();

            return View(food);
        }

        // 📌 GET: Foods/Create (Форма за създаване на храна)
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.FoodCategories, "Id", "Name");
            return View();
        }

        // 📌 POST: Foods/Create (Създаване на храна)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CaloriesPer100g,CategoryId")] Foods food)
        {
            ModelState.Remove("Category"); // За да избегнем грешка при валидация

            if (ModelState.IsValid)
            {
                _context.Add(food);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(_context.FoodCategories, "Id", "Name", food.CategoryId);
            return View(food);
        }

        // 📌 GET: Foods/Edit/5 (Редактиране на храна)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var food = await _context.Foods.FindAsync(id);
            if (food == null) return NotFound();

            ViewBag.Categories = new SelectList(_context.FoodCategories, "Id", "Name", food.CategoryId);
            return View(food);
        }

        // 📌 POST: Foods/Edit/5 (Запазване на редактирана храна)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CaloriesPer100g,CategoryId")] Foods food)
        {
            if (id != food.Id) return NotFound();

            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(food);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodExists(food.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(_context.FoodCategories, "Id", "Name", food.CategoryId);
            return View(food);
        }

        // 📌 GET: Foods/Delete/5 (Изтриване на храна)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var food = await _context.Foods
                .Include(f => f.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (food == null) return NotFound();

            return View(food);
        }

        // 📌 POST: Foods/Delete/5 (Потвърждение на изтриване)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            if (food != null)
            {
                _context.Foods.Remove(food);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool FoodExists(int id)
        {
            return _context.Foods.Any(e => e.Id == id);
        }
    }
}
