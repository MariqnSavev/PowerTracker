namespace PowerTracker.ViewModels
{
    public class EditProfileViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Name { get; internal set; }
        // Добави всички полета, които искаш да редактираш в профила.
    }
}
