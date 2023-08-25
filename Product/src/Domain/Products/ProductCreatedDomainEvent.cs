using Domain.Primitives;
using Domain.Products;

namespace Domain.Orders;

public record ProductCreatedDomainEvent(
    Guid Id,
    ProductId ProductId,
    string Name,
    Money Price,
    Sku Sku
 ) : DomainEvent(Id);