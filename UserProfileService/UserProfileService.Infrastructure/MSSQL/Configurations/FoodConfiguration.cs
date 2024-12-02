using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserProfileService.Domain.Entities;

namespace UserProfileService.Infrastructure.MSSQL.Configurations
{
    public class FoodConfiguration : IEntityTypeConfiguration<Food>
    {
        public void Configure(EntityTypeBuilder<Food> builder)
        {
            builder.HasDiscriminator<string>("FoodType")
                .HasValue<Ingredient>("Ingredient")
                .HasValue<Dish>("Dish");
        }
    }
}


