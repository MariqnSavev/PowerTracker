using System;

namespace BoxingAppDiploma.Models
{
    public class Training
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int DurationMinutes { get; set; } // Продължителност в минути
        public int CaloriesBurned { get; set; }  // Изгорени калории
    }
}