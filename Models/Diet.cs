using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerTracker.Models
{
    public class Diet
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Моля, изберете категория.")]
        public int CategoryId { get; set; }
        public virtual FoodCategories Category { get; set; }

        [Required(ErrorMessage = "Моля, изберете храна.")]
        public int FoodId { get; set; }
        public virtual Foods Food { get; set; }

        [Required(ErrorMessage = "Моля, въведете количество.")]
        public double QuantityInGrams { get; set; }

        public double Calories { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
