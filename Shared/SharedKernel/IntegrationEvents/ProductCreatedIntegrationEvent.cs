namespace SharedKernel.IntegrationEvents
{
    public record ProductCreatedIntegrationEvent(
        Guid Id,
        string Name,
        string Currency,
        decimal Amount,
        string Sku
    );
}
