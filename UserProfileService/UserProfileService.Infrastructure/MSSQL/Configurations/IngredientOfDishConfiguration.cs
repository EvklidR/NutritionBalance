using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserProfileService.Domain.Entities;

namespace UserProfileService.Infrastructure.MSSQL.Configurations
{
    public class IngredientOfDishConfiguration : IEntityTypeConfiguration<IngredientOfDish>
    {
        public void Configure(EntityTypeBuilder<IngredientOfDish> builder)
        {
            builder.HasKey(iod => new { iod.DishId, iod.IngredientId });

            builder.HasOne<Dish>()
                .WithMany(d => d.Ingredients)
                .HasForeignKey(iod => iod.DishId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(iod => iod.Ingredient)
                .WithMany()
                .HasForeignKey(iod => iod.IngredientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
