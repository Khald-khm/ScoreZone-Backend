using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreZone.Domain.Facility;

namespace ScoreZone.Infrastructure.Configurations
{
    public class FacilityRateConfiguration : IEntityTypeConfiguration<FacilityRate>
    {
        public void Configure(EntityTypeBuilder<FacilityRate> builder)
        {
            builder.ToTable("FacilityRates");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Facility)
                .WithMany()
                .HasForeignKey(x => x.FacilityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}