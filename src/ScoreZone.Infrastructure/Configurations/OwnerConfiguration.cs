using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreZone.Domain.User.Owner;

namespace ScoreZone.Infrastructure.Configurations
{
    public class OwnerConfiguration : IEntityTypeConfiguration<OwnerEntity>
    {
        public void Configure(EntityTypeBuilder<OwnerEntity> builder)
        {
            builder.ToTable("Owners");
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Employees)
                .WithOne(e => e.Owner)
                .HasForeignKey(x => x.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasMany(x => x.FootballCourts)
                .WithOne(c => c.Owner)
                .HasForeignKey(x => x.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}