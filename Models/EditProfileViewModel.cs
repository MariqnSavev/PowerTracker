using System.ComponentModel.DataAnnotations;

public class EditProfileViewModel
{
    [Required]
    public string Name { get; set; } // Име на потребителя

    [EmailAddress]
    [Required]
    public string Email { get; set; } // Имейл на потребителя
}