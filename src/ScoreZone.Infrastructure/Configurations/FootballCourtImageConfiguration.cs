using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreZone.Domain.FootballCourt;

namespace ScoreZone.Infrastructure.Configurations
{
    public class FootballCourtImageConfiguration : IEntityTypeConfiguration<CourtImage>
    {
        public void Configure(EntityTypeBuilder<CourtImage> builder)
        {
            builder.ToTable("FootballCourtImages");
            builder.HasKey(x => x.Id);

        }
    }
}