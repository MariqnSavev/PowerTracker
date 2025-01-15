using Microsoft.AspNetCore.Mvc;
using PowerTracker.Data;
using PowerTracker.Models;
using System;
using System.Linq;

namespace PowerTracker.Controllers
{
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
            var diets = _context.Diet.ToList(); // Вземаме всички диети
            return View(diets);
        }

        // GET: Diets/Create
        public IActionResult Create()
        {
            ViewBag.FoodList = _context.Foods.ToList(); // Предоставяме всички храни за избор
            ViewBag.CategoryList = _context.FoodCategories.ToList(); // Предоставяме всички категории за избор
            return View();
        }

        // POST: Diets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Diet diet)
        {
            if (ModelState.IsValid)
            {
                // Тук използваме новите имена на свойствата от модела
                var food = _context.Foods.FirstOrDefault(f => f.Id == diet.FoodId); // FoodId вместо IDFood
                var category = _context.FoodCategories.FirstOrDefault(c => c.Id == diet.CategoryId); // CategoryId вместо IDCategory
                if (food != null && category != null)
                {
                    // Задаваме стойностите за Food и CaloriesPer100g от Food модела
                    diet.Food = food; // Food вместо NameOfFood
                    diet.Category = category; // Category вместо NameOfCategory
                    diet.CaloriesPer100g = food.CaloriesPer100g;
                    diet.CalculateCalories(); // Изчисляваме калориите

                    diet.Date = DateTime.Now; // Задаваме текущата дата

                    _context.Add(diet); // Добавяме новата диета в контекста
                    _context.SaveChanges(); // Записваме промените в базата

                    return RedirectToAction(nameof(Index)); // Пренасочваме към Index
                }
                ModelState.AddModelError("", "Храната или категорията не са намерени.");
            }

            // Връщаме данни към изгледа при грешка
            ViewBag.FoodList = _context.Foods.ToList();
            ViewBag.CategoryList = _context.FoodCategories.ToList();
            return View(diet);
        }

        // GET: Diets/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var diet = _context.Diet.FirstOrDefault(d => d.Id == id);
            if (diet == null)
                return NotFound();

            ViewBag.FoodList = _context.Foods.ToList();
            ViewBag.CategoryList = _context.FoodCategories.ToList();
            return View(diet);
        }

        // POST: Diets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Diet diet)
        {
            if (id != diet.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var existingDiet = _context.Diet.FirstOrDefault(d => d.Id == id);
                if (existingDiet != null)
                {
                    var food = _context.Foods.FirstOrDefault(f => f.Id == diet.FoodId);
                    var category = _context.FoodCategories.FirstOrDefault(c => c.Id == diet.CategoryId);

                    if (food != null && category != null)
                    {
                        existingDiet.Food = food;
                        existingDiet.Category = category;
                        existingDiet.QuantityInGrams = diet.QuantityInGrams;
                        existingDiet.CaloriesPer100g = food.CaloriesPer100g;
                        existingDiet.CalculateCalories(); // Изчисляваме калориите
                        existingDiet.Date = DateTime.Now;

                        _context.SaveChanges();
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError("", "Храната или категорията не са намерени.");
                }
            }

            ViewBag.FoodList = _context.Foods.ToList();
            ViewBag.CategoryList = _context.FoodCategories.ToList();
            return View(diet);
        }

        // GET: Diets/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var diet = _context.Diet.FirstOrDefault(d => d.Id == id);
            if (diet == null)
                return NotFound();

            return View(diet);
        }

        // POST: Diets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var diet = _context.Diet.FirstOrDefault(d => d.Id == id);
            if (diet != null)
            {
                _context.Diet.Remove(diet); // Премахваме диетата от базата
                _context.SaveChanges(); // Записваме промените в базата
            }

            return RedirectToAction(nameof(Index)); // Пренасочваме към Index
        }
    }
}
