using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PowerTracker.Models
{
    public class Diet
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Моля, изберете категория.")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual FoodCategories Category { get; set; }

        [Required(ErrorMessage = "Моля, изберете храна.")]
        public int FoodId { get; set; }

        [ForeignKey("FoodId")]
        public virtual Foods Food { get; set; }

        [Required(ErrorMessage = "Моля, въведете количество.")]
        [Range(1, 2000, ErrorMessage = "Количеството трябва да бъде между 1 и 2000 грама.")]
        public double QuantityInGrams { get; set; }

        public double Calories { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        // 🚀 Foreign Key към потребителя
        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }
    }
}















































