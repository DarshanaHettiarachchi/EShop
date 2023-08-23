using MediatR;

namespace Application.Products.Get;

public record GetAllProductQuery() : IRequest<IList<ProductDetail>>;

public record ProductDetail(
    Guid Id,
    string Name,
    string Sku,
    string Currency,
    decimal Amount);
