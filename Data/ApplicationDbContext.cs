using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BoxingAppDiploma.Models;

namespace BoxingAppDiploma.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
     
        public DbSet<BoxingAppDiploma.Models.Diet> Diet { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<BoxingAppDiploma.Models.Goal>? Goal { get; set; }
    }
}