using Application.Data;
using Domain.Orders;
using MediatR;

namespace Application.Orders.Create;

internal sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
{

    private readonly IOrderRepository _orderRepository;
    private readonly IOrderSummaryRepository _orderSummaryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IOrderSummaryRepository orderSummaryRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _orderSummaryRepository = orderSummaryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {

        var order = Order.Create();

        _orderRepository.Add(order);

        _orderSummaryRepository.Add(new OrderSummary(order.Id.Value, 0));

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
