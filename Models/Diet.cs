using System;
using System.ComponentModel.DataAnnotations;

namespace PowerTracker.Models
{
    public class Diet
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Моля, въведете храна.")]
        public int FoodId { get; set; }
        public virtual Foods Food { get; set; }

        [Required(ErrorMessage = "Моля, въведете калории на 100 грама.")]
        [Range(0, 1000, ErrorMessage = "Калориите трябва да бъдат между 0 и 1000.")]
        public double CaloriesPer100g { get; set; }

        [Required(ErrorMessage = "Моля, въведете количество.")]
        [Range(1, 10000, ErrorMessage = "Количеството трябва да бъде между 1 и 10,000 грама.")]
        public double QuantityInGrams { get; set; }

        public double Calories { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public void CalculateCalories()
        {
            Calories = (QuantityInGrams / 100) * CaloriesPer100g;
        }
    }
}
