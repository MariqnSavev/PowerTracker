using System.ComponentModel.DataAnnotations;

namespace PowerTracker.Models
{
    public class Food
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Името е задължително")]
        public string Name { get; set; } = string.Empty;

        [Range(0, 1000, ErrorMessage = "Невалиден брой калории")]
        public double Calories { get; set; }

        [Range(0, 100, ErrorMessage = "Невалидно количество протеин")]
        public double Protein { get; set; }

        [Range(0, 100, ErrorMessage = "Невалидно количество мазнини")]
        public double Fat { get; set; }

        [Range(0, 100, ErrorMessage = "Невалидно количество въглехидрати")]
        public double Carbs { get; set; }

        public string? Brand { get; set; }
        public string? Barcode { get; set; }
        public double ServingSize { get; set; } = 100;
        public string? PhotoUrl { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
        public double TotalCalories => Math.Round(Calories * (ServingSize / 100.0), 2);
        public double TotalProtein => Math.Round(Protein * (ServingSize / 100.0), 2);
        public double TotalFat => Math.Round(Fat * (ServingSize / 100.0), 2);
        public double TotalCarbs => Math.Round(Carbs * (ServingSize / 100.0), 2);
    }
}