using SharedKernel.IntegrationEvents;

namespace Application.Common.Interfaces
{
    public interface IProductCreatedIntegrationEventPublisherService
    {
        void PublishEvent(ProductCreatedIntegrationEvent integrationEvent);

    }
}

