using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreZone.Domain.Facility;

namespace ScoreZone.Infrastructure.Configurations
{
    public class FacilityImageConfiguration : IEntityTypeConfiguration<FacilityImage>
    {
        public void Configure(EntityTypeBuilder<FacilityImage> builder)
        {
            builder.ToTable("FacilityImages");
            builder.HasKey(x => x.Id);

        }
    }
}