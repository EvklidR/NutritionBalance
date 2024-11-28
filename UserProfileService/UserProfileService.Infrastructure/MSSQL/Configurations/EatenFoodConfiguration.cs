using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserProfileService.Domain.Entities;

namespace UserProfileService.Infrastructure.MSSQL.Configurations
{
    public class EatenFoodConfiguration : IEntityTypeConfiguration<EatenFood>
    {
        public void Configure(EntityTypeBuilder<EatenFood> builder)
        {
            builder.HasKey(ef => new { ef.FoodId, ef.MealId });

            builder.HasOne(ef => ef.Food)
                .WithMany()
                .HasForeignKey(ef => ef.FoodId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Meal>()
                .WithMany(m => m.Foods)
                .HasForeignKey(ef => ef.MealId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
