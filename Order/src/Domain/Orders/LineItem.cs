using SharedKernel;

namespace Domain.Orders;

public class LineItem
{
    internal LineItem(LineItemId id, OrderId orderId, ProductId productId, Money price)
    {
        Id = id;
        OrderId = orderId;
        ProductId = productId;
        Price = price;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private LineItem()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public LineItemId Id { get; private set; }

    public OrderId OrderId { get; private set; }

    public ProductId ProductId { get; private set; }

    public Money Price { get; private set; }
}