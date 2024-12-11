namespace PowerTracker.Models
{
    public class ManageUserViewModel
    {
        public string Id { get; set; } // Идентификатор на потребителя
        public string UserName { get; set; } // Име на потребителя
        public string Email { get; set; } // Имейл на потребителя
        public List<string> Roles { get; set; } // Списък с роли на потребителя
    }
}
