using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreZone.Domain.FootballCourt;

namespace ScoreZone.Infrastructure.Configurations
{
    public class FootballCourtConfiguration : IEntityTypeConfiguration<FootballCourtEntity>
    {
        public void Configure(EntityTypeBuilder<FootballCourtEntity> builder)
        {
            builder.ToTable("FootballCourts");
            builder.HasKey(x => x.Id);
            
            builder.HasMany(x => x.CourtImages)
                .WithOne(i => i.FootballCourt)
                .HasForeignKey(x => x.CourtId)
                .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}