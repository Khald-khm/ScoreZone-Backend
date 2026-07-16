using ScoreZone.Domain.Notification.Enums;
using ScoreZone.Domain.Shared.Entities;

namespace ScoreZone.Domain.Notification
{
    public class NotificationEntity : Entity
    {
        public Guid RecipientId { get; set; }
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public bool IsRead { get; set; } = false;
        public NotificationType Type { get; set; }
        public Guid? RelatedEntityId { get; set; }

        private NotificationEntity() {}

        public NotificationEntity(Guid recipientId, string title, string message, NotificationType type, Guid? relatedEntityId)
        {
            RecipientId = recipientId;
            Title = title;
            Message = message;
            Type = type;
            RelatedEntityId = relatedEntityId ?? null;
        }

        public void MarkAsRead()
        {
            IsRead = true;
        }
    }
}
