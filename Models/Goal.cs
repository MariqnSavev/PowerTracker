namespace BoxingAppDiploma.Models
{
    public class Goal
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime TargetDate { get; set; }
        public bool IsAchieved { get; set; }
        public int UserId { get; set; }
       
    }
}
