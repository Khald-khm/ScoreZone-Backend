using ScoreZone.Domain.Shared.Interfaces;
using MediatR;

namespace ScoreZone.Application.Shared.Wrappers
{
    
    public sealed class DomainEventNotification<TDomainEvent> : INotification
        where TDomainEvent : IDomainEvent
    {
        public TDomainEvent DomainEvent { get;}

        public DomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }
    }
}