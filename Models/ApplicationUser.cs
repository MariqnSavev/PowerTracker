
using Microsoft.AspNetCore.Identity;

namespace PowerTracker.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}