using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreZone.Domain.Notification;

namespace ScoreZone.Infrastructure.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<NotificationEntity>
    {
        public void Configure(EntityTypeBuilder<NotificationEntity> builder)
        {
            builder.ToTable("Notifications");
            builder.HasKey(x => x.Id);

        }
    }
}