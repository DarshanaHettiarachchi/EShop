using Domain.Primitives;
using SharedKernel;

namespace Domain.Orders;

public class Order : Entity
{
    private readonly List<LineItem> _lineItems = new();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Order()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public OrderId Id { get; private set; }

    public IReadOnlyList<LineItem> LineItems => _lineItems.ToList();

    public static Order Create()
    {
        var order = new Order
        {
            Id = new OrderId(Guid.NewGuid()),
        };

        order.Raise(new OrderCreatedDomainEvent(Guid.NewGuid(), order.Id));

        return order;
    }

    public void Add(ProductId productId, Money price)
    {
        var lineItem = new LineItem(
            new LineItemId(Guid.NewGuid()),
            Id,
            productId,
            price);

        _lineItems.Add(lineItem);
    }

    public void RemoveLineItem(LineItemId lineItemId, IOrderRepository orderRepository)
    {
        if (orderRepository.HasOneLineItem(this))
        {
            return;
        }

        var lineItem = _lineItems.FirstOrDefault(li => li.Id == lineItemId);

        if (lineItem is null)
        {
            return;
        }

        _lineItems.Remove(lineItem);

        Raise(new LineItemRemovedDomainEvent(Guid.NewGuid(), Id, lineItem.Id));
    }
}