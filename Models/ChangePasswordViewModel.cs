using System.ComponentModel.DataAnnotations;

public class ChangePasswordViewModel
{
    [Required]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; } // Текуща парола

    [Required]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "Паролата трябва да е поне {2} символа дълга.", MinimumLength = 6)]
    public string NewPassword { get; set; } // Нова парола

    [DataType(DataType.Password)]
    [Display(Name = "Потвърдете новата парола")]
    [Compare("NewPassword", ErrorMessage = "Новата парола и потвърдителната парола не съвпадат.")]
    public string ConfirmPassword { get; set; } // Потвърдителна парола
}
