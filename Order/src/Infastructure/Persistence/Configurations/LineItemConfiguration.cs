using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel;

namespace Persistence.Configurations;

internal class LineItemConfiguration : IEntityTypeConfiguration<LineItem>
{
    public void Configure(EntityTypeBuilder<LineItem> builder)
    {
        builder.HasKey(li => li.Id);

        builder.Property(li => li.Id).HasConversion(
            lineItemId => lineItemId.Value,
            value => new LineItemId(value));

        builder.Property(li => li.ProductId).HasConversion(
            productId => productId.Value,
            value => new ProductId(value));

        builder.OwnsOne(li => li.Price);
    }
}
