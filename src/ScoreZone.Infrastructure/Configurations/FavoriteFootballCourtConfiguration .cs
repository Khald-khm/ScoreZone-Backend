using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreZone.Domain.FootballCourt;

namespace ScoreZone.Infrastructure.Configurations
{
    public class FavoriteFootballCourtConfiguration  : IEntityTypeConfiguration<FavoriteCourt>
    {
        public void Configure(EntityTypeBuilder<FavoriteCourt> builder)
        {
            builder.ToTable("FavoriteFootballCourts");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.FootballCourt)
                .WithMany()
                .HasForeignKey(x => x.CourtId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}