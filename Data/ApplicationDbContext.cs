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

        public DbSet<Diet> Diets { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Foods> Foods { get; set; }
        public DbSet<FoodCategories> FoodCategories { get; set; }
        public DbSet<Goal> Goals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🚀 Foreign Key за Training (връзка с потребителя)
            modelBuilder.Entity<Training>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Ако потребителят бъде изтрит, тренировките му също се трият

            //// 🚀 Foreign Key за Diet (връзка с Category)
            //modelBuilder.Entity<Diet>()
            //    .HasOne(d => d.Category)
            //    .WithMany()
            //    .HasForeignKey(d => d.CategoryId)
            //    .OnDelete(DeleteBehavior.Restrict);

            // 🚀 Foreign Key за Diet (връзка с Food)
            modelBuilder.Entity<Diet>()
                .HasOne(d => d.Food)
                .WithMany()
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🚀 Foreign Key за Diet (връзка с потребителя)
            modelBuilder.Entity<Diet>()
                .HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🚀 Foreign Key за Goal (връзка с потребителя)
            modelBuilder.Entity<Goal>()
                .HasOne(g => g.User)
                .WithMany()
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}























































































































































































































































































































