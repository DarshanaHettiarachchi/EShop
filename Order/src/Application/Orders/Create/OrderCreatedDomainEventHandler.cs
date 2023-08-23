using Domain.Orders;
using MediatR;

namespace Application.Orders.Create;

internal sealed class OrderCreatedDomainEventHandler
    : INotificationHandler<OrderCreatedDomainEvent>
{
    public Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
