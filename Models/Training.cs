namespace BoxingAppDiploma.Models
{
    public class Training
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int DurationMinutes { get; set; } // Duration in minutes
        public int CaloriesBurned { get; set; }
    }
}
