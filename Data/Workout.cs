using System;
using System.ComponentModel.DataAnnotations;

namespace TrainigBoxingApp.Models
{
    public class Workout
    {
        public int Id { get; set; }

        public DateTime Date { get; set; } // Дата на тренировката

        [Required(ErrorMessage = "Моля, въведете тип на тренировката.")]
        public string Type { get; set; } = string.Empty; // Тип на тренировката

        [Range(1, 300, ErrorMessage = "Продължителността трябва да бъде между 1 и 300 минути.")]
        public int Duration { get; set; } // Продължителност в минути

        public string Notes { get; set; } = string.Empty; // Допълнителни бележки

        public string Description { get; set; } = string.Empty; // Описание на тренировката

        [Required(ErrorMessage = "Моля, въведете ID на потребителя.")]
        public int UserId { get; set; } // ID на потребителя 

        [Required]
        public User User { get; set; } // Връзка с модела User
    }
}
