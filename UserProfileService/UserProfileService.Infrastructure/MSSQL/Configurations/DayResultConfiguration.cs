using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserProfileService.Domain.Entities;

namespace UserProfileService.Infrastructure.MSSQL.Configurations
{
    public class DayResultConfiguration : IEntityTypeConfiguration<DayResult>
    {
        public void Configure(EntityTypeBuilder<DayResult> builder)
        {
            builder.Property(dr => dr.GlassesOfWater).HasDefaultValue(0);

            builder.HasMany(dr => dr.Meals)
                .WithOne()
                .HasForeignKey(m => m.DayId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
