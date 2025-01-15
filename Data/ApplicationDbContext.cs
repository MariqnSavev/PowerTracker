using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using PowerTracker.Data;
using PowerTracker.Models;
using System.ComponentModel;
using System;

namespace PowerTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
     
        public DbSet<PowerTracker.Models.Diet> Diet { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<Foods> Foods { get; set; }
        public DbSet<PowerTracker.Models.FoodCategories>? FoodCategories { get; set; }

       


    }
}