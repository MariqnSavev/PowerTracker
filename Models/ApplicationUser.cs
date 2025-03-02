
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PowerTracker.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
    }
}