using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PowerTracker.Models
{
    public class Training
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Моля, въведете дата на тренировката.")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Моля, въведете описание на тренировката.")]
        [StringLength(500, ErrorMessage = "Описанието не може да бъде по-дълго от 500 символа.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Моля, изберете вид на тренировката.")]
        public string Activity { get; set; } // Вид на тренировката (за падащото меню)

        [Required(ErrorMessage = "Моля, въведете продължителността на тренировката.")]
        [Range(1, 300, ErrorMessage = "Продължителността трябва да бъде между 1 и 300 минути.")]
        public int DurationMinutes { get; set; }

        [Required(ErrorMessage = "Моля, въведете вашето тегло.")]
        [Range(30, 300, ErrorMessage = "Теглото трябва да бъде между 30 и 300 кг.")]
        public double WeightInKg { get; set; } // Тегло на потребителя

        public double CaloriesBurned { get; set; } // Изчислени изгорени калории

        // 🚀 Foreign Key към `IdentityUser`
        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; } // Връзка с ASP.NET Identity
    }
}



















































































































































