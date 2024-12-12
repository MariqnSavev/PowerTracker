using System;
using System.ComponentModel.DataAnnotations;

namespace PowerTracker.Models
{
    public class Diet
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Моля, изберете храна.")]
        public string FoodName { get; set; } = string.Empty; // Гарантира, че има стойност по подразбиране

        [Required(ErrorMessage = "Моля, въведете количество в грамове.")]
        [Range(1, 10000, ErrorMessage = "Моля, въведете валидно количество (1-10000g).")]
        public double QuantityInGrams { get; set; } // Количество в грамове

        [Range(0, 5000, ErrorMessage = "Калориите трябва да са в разумен диапазон.")]
        public double Calories { get; set; } // Изчислени калории въз основа на количеството

        [Required]
        public DateTime Date { get; set; } = DateTime.Now; // Дата на записа, зададена по подразбиране
    }
}


