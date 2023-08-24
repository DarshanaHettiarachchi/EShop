using Application.Common.Interfaces;
using Application.Data;
using Application.Models;
using Application.Orders.Create;
using Domain.Orders;
using Moq;
using SharedKernel;

namespace Application.UnitTests.Orders.AddProduct
{
    public class AddProductCommandHandlerTests
    {

        private readonly Mock<IOrderRepository> _orderRepository;
        private readonly Mock<IOrderSummaryRepository> _orderSummaryRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IOrderedProductService> _orderedProductService;
        public AddProductCommandHandlerTests()
        {
            _orderRepository = new();
            _orderSummaryRepository = new();
            _unitOfWork = new();
            _orderedProductService = new();
        }

        [Fact]
        public async Task Handle_Should_CallAddOnOrderRepository_WhenProductIsNotNull()
        {
            Guid guid = Guid.NewGuid();

            var orderedProduct = new OrderedProduct(
                  guid,
                  "Book",
                  "LKR",
                  120
             );

            var command = new AddProductCommand(new ProductId(guid));

            _orderRepository.Setup(
               x => x.Add(
                   It.IsAny<Order>()));

            _orderedProductService.Setup(
                x => x.GetOrderedProduct(
                    It.IsAny<Guid>())).ReturnsAsync(orderedProduct);

            var handler = new AddProductCommandHandler(
                _orderRepository.Object,
                _orderSummaryRepository.Object,
                _unitOfWork.Object,
                _orderedProductService.Object
            );

            await handler.Handle(command, default);

            _orderRepository.Verify(
             x => x.Add(It.Is<Order>(o => o.LineItems[0].ProductId.Value == guid)),
             Times.Once);
        }

    }
}
