using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PowerTracker.Models
{
    public class Food
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double Calories { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Carbs { get; set; }

        public string? Brand { get; set; }
        public string? Barcode { get; set; }
        public double ServingSize { get; set; }

        public string? PhotoUrl { get; set; }

        [Required]
        public string UserId { get; set; }  // Foreign Key

        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }  // Навигационно свойство

        public DateTime DateAdded { get; set; }


        [NotMapped]
        public double TotalCalories => Math.Round(Calories * (ServingSize / 100.0), 2);
        [NotMapped]
        public double TotalProtein => Math.Round(Protein * (ServingSize / 100.0), 2);
        [NotMapped]
        public double TotalFat => Math.Round(Fat * (ServingSize / 100.0), 2);
        [NotMapped]
        public double TotalCarbs => Math.Round(Carbs * (ServingSize / 100.0), 2);

    }
}



