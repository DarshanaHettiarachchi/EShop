using Application.Common.Interfaces;
using Domain.Orders;
using MediatR;
using SharedKernel.IntegrationEvents;

namespace Application.Orders.Create;

internal sealed class ProductCreatedDomainEventHandler
    : INotificationHandler<ProductCreatedDomainEvent>
{

    private readonly IProductCreatedIntegrationEventPublisherService _publisherService;

    public ProductCreatedDomainEventHandler(
        IProductCreatedIntegrationEventPublisherService publisherService
    )
    {
        _publisherService = publisherService;
    }
    public Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _publisherService.PublishEvent(new ProductCreatedIntegrationEvent(
            notification.ProductId.Value,
            notification.Name,
            notification.Price.Currency,
            notification.Price.Amount,
            notification.Sku.Value
        ));
        return Task.CompletedTask;
    }
}
