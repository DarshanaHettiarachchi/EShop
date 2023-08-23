namespace Application.Data;

using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
public interface IApplicationDbContext
{
    DbSet<Order> Orders { get; set; }

    DbSet<OrderSummary> OrderSummaries { get; set; }

    DbSet<LineItem> LineItems { get; set; }

    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
