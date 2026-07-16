using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreZone.Domain.FootballCourt;

namespace ScoreZone.Infrastructure.Configurations
{
    public class CourtRateConfiguration : IEntityTypeConfiguration<CourtRate>
    {
        public void Configure(EntityTypeBuilder<CourtRate> builder)
        {
            builder.ToTable("FootballCourtRates");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.FootballCourt)
                .WithMany()
                .HasForeignKey(x => x.CourtId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}