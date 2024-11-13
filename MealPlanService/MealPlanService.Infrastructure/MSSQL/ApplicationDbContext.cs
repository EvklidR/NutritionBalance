using MealPlanService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MealPlanService.Infrastructure.MSSQL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<MealPlan> MealPlans { get; set; } = null!;
        public DbSet<MealPlanDay> MealPlanDays { get; set; } = null!;
        public DbSet<NutrientOfDay> NutrientsOfDay { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MealPlan>()
                .ToTable("MealPlans", "MealPlanServiceSchema");

            modelBuilder.Entity<MealPlanDay>()
                .ToTable("MealPlanDays", "MealPlanServiceSchema");

            modelBuilder.Entity<NutrientOfDay>()
                .ToTable("NutrientsOfDay", "MealPlanServiceSchema");


            modelBuilder.Entity<MealPlan>()
                .HasMany(mp => mp.Days)
                .WithOne()
                .HasForeignKey(mpd => mpd.MealPlanId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MealPlanDay>()
                .HasMany(mpd => mpd.NutrientsOfDay)
                .WithOne()
                .HasForeignKey(nd => nd.MealPlanDayId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MealPlanDay>()
                .HasIndex(mpd => new { mpd.MealPlanId, mpd.NumberOfDay })
                .IsUnique();

            modelBuilder.Entity<NutrientOfDay>()
                .HasIndex(nd => new { nd.MealPlanDayId, nd.NutrientType })
                .IsUnique();
        }
    }
}
