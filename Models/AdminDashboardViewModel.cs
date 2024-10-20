namespace BoxingAppDiploma.Models
{
    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; } // Общо потребители
        public int ActiveUsers { get; set; } // Активни потребители
        public int InactiveUsers { get; set; } // Неактивни потребители
        public List<ApplicationUser> Users { get; set; } // Списък с потребители
    }
}
