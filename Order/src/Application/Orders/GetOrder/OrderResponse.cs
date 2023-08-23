namespace Application.Orders.GetOrder;

public record OrderResponse(Guid Id, List<LineItemResponse> LineItems);