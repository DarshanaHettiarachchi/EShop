using Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Get;

internal sealed class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, IList<ProductDetail>>
{
    private readonly IApplicationDbContext _context;

    public GetAllProductQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IList<ProductDetail>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var products = await _context
            .Products
            .AsNoTracking()
            .Select(p => new ProductDetail(
                p.Id.Value,
                p.Name,
                p.Sku.Value,
                p.Price.Currency,
                p.Price.Amount))
            .ToListAsync<ProductDetail>(cancellationToken);

        return products;
    }
}
