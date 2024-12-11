using System;
using System.ComponentModel.DataAnnotations;

namespace PowerTracker.Models
{
    public class Training
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Моля, въведете дата на тренировката.")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Моля, въведете описание на тренировката.")]
        [StringLength(500, ErrorMessage = "Описанието не може да бъде по-дълго от 500 символа.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Моля, въведете продължителността на тренировката.")]
        [Range(1, 300, ErrorMessage = "Продължителността трябва да бъде между 1 и 300 минути.")]
        public int DurationMinutes { get; set; }

        [Required(ErrorMessage = "Моля, въведете изгорените калории.")]
        [Range(0, 1000, ErrorMessage = "Изгорените калории трябва да бъдат между 0 и 1000.")]
        public int CaloriesBurned { get; set; }
    }
}