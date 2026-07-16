using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreZone.Domain.Facility;

namespace ScoreZone.Infrastructure.Configurations
{
    public class FacilityConfiguration : IEntityTypeConfiguration<FacilityEntity>
    {
        public void Configure(EntityTypeBuilder<FacilityEntity> builder)
        {
            builder.ToTable("Facilities");

            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.FootballCourts)
                .WithOne(c => c.Facitlity)
                .HasForeignKey(x => x.FacilityId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.FacitlityImages)
                .WithOne(i => i.Facility)
                .HasForeignKey(x => x.FacilityId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}