using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreZone.Domain.Reservation;

namespace ScoreZone.Infrastructure.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<ReservationEntity>
    {
        public void Configure(EntityTypeBuilder<ReservationEntity> builder)
        {
            builder.ToTable("Reservations");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.FootballCourt)
                .WithMany(c => c.Reservations)
                .HasForeignKey(x => x.CourtId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(x => x.Player)
                .WithMany(p => p.Reservations)
                .HasForeignKey(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}