using Application.Data;
using Domain.Orders;
using Domain.Primitives;
using Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infastructure;

public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;

    public ApplicationDbContext(DbContextOptions options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder?.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderSummary> OrderSummaries { get; set; }

    public DbSet<LineItem> LineItems { get; set; }

    public DbSet<Product> Products { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var domainEvents = ChangeTracker.Entries<Entity>()
            .Select(e => e.Entity)
            .Where(e => e.GetDomainEvents().Count != 0)
            .SelectMany(e => e.GetDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }
}
