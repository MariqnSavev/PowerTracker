using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    // Можеш да добавиш свои свойства, ако е необходимо
    public string FullName { get; set; } // Име на потребителя
}