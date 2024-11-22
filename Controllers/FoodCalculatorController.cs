
using BoxingAppDiploma.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BoxingAppDiploma.Controllers
{
    public class FoodCalculatorController : Controller
    {
        private static List<FoodItem> FoodDatabase = new List<FoodItem>
{
           new FoodItem { Id = 1, Name = "Chicken Breast", CaloriesPer100g = 165 },
           new FoodItem { Id = 2, Name = "Rice", CaloriesPer100g = 130 },
           new FoodItem { Id = 3, Name = "Apple", CaloriesPer100g = 52 },
           new FoodItem { Id = 4, Name = "Avocado", CaloriesPer100g = 160 },
           new FoodItem { Id = 5, Name = "Banana", CaloriesPer100g = 89 },
           new FoodItem { Id = 6, Name = "Bread", CaloriesPer100g = 265 },
           new FoodItem { Id = 7, Name = "Cheese", CaloriesPer100g = 402 },
           new FoodItem { Id = 8, Name = "Egg", CaloriesPer100g = 155 },
           new FoodItem { Id = 9, Name = "Milk", CaloriesPer100g = 42 },
           new FoodItem { Id = 10, Name = "Pasta", CaloriesPer100g = 131 },
           new FoodItem { Id = 11, Name = "Potatoes", CaloriesPer100g = 77 },
           new FoodItem { Id = 12, Name = "Salmon", CaloriesPer100g = 208 },
           new FoodItem { Id = 13, Name = "Spinach", CaloriesPer100g = 23 },
           new FoodItem { Id = 14, Name = "Tomato", CaloriesPer100g = 18 },
           new FoodItem { Id = 15, Name = "Yogurt", CaloriesPer100g = 59 },
           new FoodItem { Id = 16, Name = "Beef Steak", CaloriesPer100g = 271 },
           new FoodItem { Id = 17, Name = "Carrot", CaloriesPer100g = 41 },
           new FoodItem { Id = 18, Name = "Cucumber", CaloriesPer100g = 15 },
           new FoodItem { Id = 19, Name = "Onion", CaloriesPer100g = 40 } ,
           new FoodItem { Id = 20, Name = "Garlic", CaloriesPer100g = 149 },
           new FoodItem { Id = 21, Name = "Peanut Butter", CaloriesPer100g = 588 },
           new FoodItem { Id = 22, Name = "Honey", CaloriesPer100g = 304 },
           new FoodItem { Id = 23, Name = "Oats", CaloriesPer100g = 389 },
           new FoodItem { Id = 24, Name = "Almonds", CaloriesPer100g = 576 },
           new FoodItem { Id = 25, Name = "Walnuts", CaloriesPer100g = 654 },
           new FoodItem { Id = 26, Name = "Dark Chocolate", CaloriesPer100g = 546 },
           new FoodItem { Id = 27, Name = "Sweet Potato", CaloriesPer100g = 86 },
           new FoodItem { Id = 28, Name = "Quinoa", CaloriesPer100g = 120 },
           new FoodItem { Id = 29, Name = "Turkey Breast", CaloriesPer100g = 135 },
           new FoodItem { Id = 30, Name = "Shrimp", CaloriesPer100g = 99 },
           new FoodItem { Id = 31, Name = "Tofu", CaloriesPer100g = 76 },
           new FoodItem { Id = 32, Name = "Green Beans", CaloriesPer100g = 31 },
           new FoodItem { Id = 33, Name = "Lentils", CaloriesPer100g = 116 },
           new FoodItem { Id = 34, Name = "Broccoli", CaloriesPer100g = 34 },
           new FoodItem { Id = 35, Name = "Cauliflower", CaloriesPer100g = 25 },
           new FoodItem { Id = 36, Name = "Peas", CaloriesPer100g = 81 },
           new FoodItem { Id = 37, Name = "Coconut Oil", CaloriesPer100g = 892 },
           new FoodItem { Id = 38, Name = "Olive Oil", CaloriesPer100g = 884 },
           new FoodItem { Id = 39, Name = "Mushrooms", CaloriesPer100g = 22 },
           new FoodItem { Id = 40, Name = "Pineapple", CaloriesPer100g = 50 }
        };

        [HttpGet]
        public IActionResult Search(string query)
        {
            var results = string.IsNullOrEmpty(query)
                ? FoodDatabase
                : FoodDatabase.Where(f => f.Name.ToLower().Contains(query.ToLower())).ToList();

            ViewBag.FoodDatabase = results;
            ViewBag.SearchQuery = query;
            return View("Index", new FoodItem());
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.FoodDatabase = FoodDatabase;
            return View(new FoodItem());
        }

        [HttpPost]
        public IActionResult Calculate(FoodItem model)
        {
            var food = FoodDatabase.FirstOrDefault(f => f.Id == model.Id);
            if (food != null)
            {
                model.Name = food.Name;
                model.CaloriesPer100g = food.CaloriesPer100g;
                model.TotalCalories = (food.CaloriesPer100g * model.QuantityInGrams) / 100;
            }
            else
            {
                ModelState.AddModelError("", "Food item not found.");
            }

            ViewBag.FoodDatabase = FoodDatabase;
            return View("Index", model);
        }
    }
}