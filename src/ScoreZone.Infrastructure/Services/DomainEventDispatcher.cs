using MediatR;
using ScoreZone.Domain.Shared.Interfaces;
using ScoreZone.Application.Shared.Wrappers;

namespace ScoreZone.Infrastructure.Services
{
    public class DomainEventDispatcher
    {
        private readonly IMediator _mediator;

        public DomainEventDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken)
        {
            foreach(var domainEvent in domainEvents)
            {
                var notificationType = typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType());

                var notification = Activator.CreateInstance(notificationType, domainEvent);

                await _mediator.Publish(notification!, cancellationToken);
            }
        }
    }
}