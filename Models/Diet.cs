using System;
using System.ComponentModel.DataAnnotations;

namespace PowerTracker.Models
{
    public class Diet
    {
        public int Id { get; set; } // Уникален идентификатор

        [Required(ErrorMessage = "Моля, въведете име на храната.")]
        public string Name { get; set; } // Име на храната

        [Required(ErrorMessage = "Моля, въведете калории на 100 грама.")]
        [Range(0, 1000, ErrorMessage = "Калориите трябва да бъдат между 0 и 1000.")]
        public double CaloriesPer100g { get; set; } // Калории на 100 грама

        [Required(ErrorMessage = "Моля, въведете количество.")]
        [Range(1, 10000, ErrorMessage = "Количеството трябва да бъде между 1 и 10,000 грама.")]
        public double QuantityInGrams { get; set; } // Количество в грамове

        public double Calories { get; set; } // Общо калории

        public DateTime Date { get; set; } = DateTime.Now; // Дата на записа
    }
}