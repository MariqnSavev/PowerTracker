using System;
using System.ComponentModel.DataAnnotations;

namespace BoxingAppDiploma.Models
{
    public class Diet
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(500)]
        public string MealDescription { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Calories must be greater than 0.")]
        public int Calories { get; set; }
    }
}