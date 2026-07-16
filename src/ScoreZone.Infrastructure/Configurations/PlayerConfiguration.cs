using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreZone.Domain.User.Player;

namespace ScoreZone.Infrastructure.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<PlayerEntity>
    {
        public void Configure(EntityTypeBuilder<PlayerEntity> builder)
        {
            builder.ToTable("Players");
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.FavoriteCourts)
                .WithOne(c => c.Player)
                .HasForeignKey(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(x => x.FacilityRates)
                .WithOne(f => f.Player)
                .HasForeignKey(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.CourteRates)
                .WithOne(c => c.Player)
                .HasForeignKey(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}