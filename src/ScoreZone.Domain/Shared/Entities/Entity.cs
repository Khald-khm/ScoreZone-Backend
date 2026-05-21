using ScoreZone.Domain.Shared.Interfaces;

namespace ScoreZone.Domain.Shared.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();


        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; protected set; } = DateTime.UtcNow;

        public bool IsDeleted { get; protected set; } = false;
        public DateTime? DeletedAt { get; protected set; }


        private readonly List<IDomainEvent> _domainEvent = new ();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvent.AsReadOnly();



        public virtual void Delete()
        {
            IsDeleted = true;
            DeletedAt = DateTime.Now;
        }


        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvent.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvent.Clear();
        }


    }
}