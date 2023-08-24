using Application.Common.Interfaces;
using Application.Data;
using Application.Models;
using Domain.Orders;
using MediatR;
using SharedKernel;

namespace Application.Orders.Create;

internal sealed class AddProductCommandHandler : IRequestHandler<AddProductCommand>
{

    private readonly IOrderRepository _orderRepository;
    private readonly IOrderSummaryRepository _orderSummaryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderedProductService _orderedProductService;

    public AddProductCommandHandler(
        IOrderRepository orderRepository,
        IOrderSummaryRepository orderSummaryRepository,
        IUnitOfWork unitOfWork,
        IOrderedProductService orderedProductService
    )
    {
        _orderRepository = orderRepository;
        _orderSummaryRepository = orderSummaryRepository;
        _unitOfWork = unitOfWork;
        _orderedProductService = orderedProductService;
    }

    public async Task Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        OrderedProduct? product = await _orderedProductService.GetOrderedProduct(request.ProductId.Value);

        if (product == null)
        {
            throw new InvalidOperationException($"No such Product {request.ProductId.Value}");
        }

        var order = Order.Create().Add(request.ProductId, new Money(product.Currency, product.Amount));

        _orderRepository.Add(order);

        var discountedPrice = product.Amount;

        if (product.Amount > 500)
        {
            discountedPrice = product.Amount - (product.Amount / 100) * 3;
        }

        _orderSummaryRepository.Add(new OrderSummary(order.Id.Value, discountedPrice));

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
