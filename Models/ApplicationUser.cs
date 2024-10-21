
using Microsoft.AspNetCore.Identity;

namespace BoxingAppDiploma.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}