using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerTracker.Controllers
{
    [Authorize]
    public class DietsController : Controller
    {
        // Статичен списък с храни
        private static readonly List<(string Name, double CaloriesPer100g)> FoodList = new()
        {
            ("Apple", 52),
            ("Banana", 96),
            ("Chicken Breast", 165),
            ("Rice", 130),
            ("Broccoli", 35),
            ("Cheese", 402),
            ("Eggs", 155),
            ("Fish", 206),
            ("Pasta", 131),
            ("Potato", 77),
            ("Tomato", 18),
            ("Milk", 42),
            ("Beef", 250),
            ("Chocolate", 546),
            ("Yogurt", 59),
            ("Bread", 265)
        };

        private static readonly List<Diet> DietRecords = new(); // Списък с записи за диетите

        // GET: Diets
        public IActionResult Index()
        {
            return View(DietRecords); // Показва всички записи
        }

        // GET: Diets/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || id < 0 || id >= DietRecords.Count)
            {
                return NotFound();
            }

            var diet = DietRecords.FirstOrDefault(d => d.Id == id);
            if (diet == null)
            {
                return NotFound();
            }

            return View(diet); // Показва детайли за записа
        }

        // GET: Diets/Create
        public IActionResult Create()
        {
            ViewBag.FoodList = FoodList;
            return View();
        }

        // POST: Diets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Diet diet)
        {
            if (ModelState.IsValid)
            {
                var food = FoodList.FirstOrDefault(f => f.Name == diet.FoodName);
                if (food != default)
                {
                    diet.Calories = (diet.QuantityInGrams / 100) * food.CaloriesPer100g;
                    diet.Date = DateTime.Now;

                    diet.Id = DietRecords.Count > 0 ? DietRecords.Max(d => d.Id) + 1 : 1; // Генерира ID
                    DietRecords.Add(diet);

                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Selected food not found.");
            }

            ViewBag.FoodList = FoodList;
            return View(diet);
        }

        // GET: Diets/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || id < 0 || id >= DietRecords.Count)
            {
                return NotFound();
            }

            var diet = DietRecords.FirstOrDefault(d => d.Id == id);
            if (diet == null)
            {
                return NotFound();
            }

            ViewBag.FoodList = FoodList;
            return View(diet);
        }

        // POST: Diets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Diet updatedDiet)
        {
            if (ModelState.IsValid)
            {
                var diet = DietRecords.FirstOrDefault(d => d.Id == id);
                if (diet == null)
                {
                    return NotFound();
                }

                var food = FoodList.FirstOrDefault(f => f.Name == updatedDiet.FoodName);
                if (food != default)
                {
                    diet.FoodName = updatedDiet.FoodName;
                    diet.QuantityInGrams = updatedDiet.QuantityInGrams;
                    diet.Calories = (updatedDiet.QuantityInGrams / 100) * food.CaloriesPer100g;
                    diet.Date = DateTime.Now;

                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Selected food not found.");
            }

            ViewBag.FoodList = FoodList;
            return View(updatedDiet);
        }

        // GET: Diets/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || id < 0 || id >= DietRecords.Count)
            {
                return NotFound();
            }

            var diet = DietRecords.FirstOrDefault(d => d.Id == id);
            if (diet == null)
            {
                return NotFound();
            }

            return View(diet);
        }

        // POST: Diets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var diet = DietRecords.FirstOrDefault(d => d.Id == id);
            if (diet != null)
            {
                DietRecords.Remove(diet);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
