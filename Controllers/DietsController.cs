using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using PowerTracker.Models;

namespace PowerTracker.Controllers
{
    public class DietsController : Controller
    {
        // Списък с наличните храни
        private static readonly List<Diet> FoodList = new()
        {
            new Diet { Id = 1, Name = "Apple", CaloriesPer100g = 52 },
            new Diet { Id = 2, Name = "Banana", CaloriesPer100g = 96 },
            new Diet { Id = 3, Name = "Orange", CaloriesPer100g = 43 },
            new Diet { Id = 4, Name = "Grapes", CaloriesPer100g = 69 },
            new Diet { Id = 5, Name = "Chicken Breast", CaloriesPer100g = 165 }
            // Добавете още храни тук
        };

        // Списък с диетични записи
        private static readonly List<Diet> DietRecords = new();

        // GET: Diets
        public IActionResult Index()
        {
            return View(DietRecords);
        }

        // GET: Diets/Create
        public IActionResult Create()
        {
            ViewBag.FoodList = new SelectList(FoodList, "Id", "Name");
            return View();
        }

        // POST: Diets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Diet diet)
        {
            if (ModelState.IsValid)
            {
                // Намираме храната по име (не по ID)
                var food = FoodList.FirstOrDefault(f => f.Name.Equals(diet.Name, StringComparison.OrdinalIgnoreCase));
                if (food != null)
                {
                    // Задаваме името на храната и калориите на 100 грама
                    diet.Name = food.Name;
                    diet.CaloriesPer100g = food.CaloriesPer100g;
                    // Изчисляваме калориите на базата на въведеното количество
                    diet.Calories = (diet.QuantityInGrams / 100) * food.CaloriesPer100g;
                    diet.Date = DateTime.Now;

                    // Определяме уникален ID за новия запис
                    diet.Id = DietRecords.Count > 0 ? DietRecords.Max(d => d.Id) + 1 : 1;
                    DietRecords.Add(diet); // Добавяме новия запис в списъка с диетични записи

                    return RedirectToAction(nameof(Index)); // Пренасочваме към индекс страницата
                }

                // Ако няма намерена храна, добавяме грешка в модела
                ModelState.AddModelError("", "Храната с това име не беше намерена.");
            }

            // Ако е имало грешки, връщаме изгледа със същите данни
            return View(diet);
        }

        // GET: Diets/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var diet = DietRecords.FirstOrDefault(d => d.Id == id);
            if (diet == null)
                return NotFound();

            ViewBag.FoodList = new SelectList(FoodList, "Id", "Name", diet.Id);
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
                var existingDiet = DietRecords.FirstOrDefault(d => d.Id == id);
                if (existingDiet != null)
                {
                    var food = FoodList.FirstOrDefault(f => f.Id == diet.Id);
                    if (food != null)
                    {
                        existingDiet.Name = food.Name;
                        existingDiet.CaloriesPer100g = food.CaloriesPer100g;
                        existingDiet.QuantityInGrams = diet.QuantityInGrams;
                        existingDiet.Calories = (diet.QuantityInGrams / 100) * food.CaloriesPer100g;
                        existingDiet.Date = DateTime.Now;

                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError("", "Избраната храна не е намерена.");
                }
            }

            ViewBag.FoodList = new SelectList(FoodList, "Id", "Name", diet.Id);
            return View(diet);
        }

        // GET: Diets/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var diet = DietRecords.FirstOrDefault(d => d.Id == id);
            if (diet == null)
                return NotFound();

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
