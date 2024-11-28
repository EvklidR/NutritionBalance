using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserProfileService.Domain.Entities;

namespace UserProfileService.Infrastructure.MSSQL.Configurations
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.HasIndex(p => new { p.UserId, p.Name }).IsUnique();

            builder.HasMany(p => p.DayResults)
                .WithOne()
                .HasForeignKey(dr => dr.ProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
