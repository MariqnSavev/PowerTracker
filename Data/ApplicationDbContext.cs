using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PowerTracker.Models;

namespace PowerTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Diet> Diet { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<Foods> Foods { get; set; }
        public DbSet<FoodCategories> FoodCategories { get; set; }
        public DbSet<Goal> Goal { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Diet>()
                .HasOne(d => d.Category)
                .WithMany()
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Diet>()
                .HasOne(d => d.Food)
                .WithMany()
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
