using Domain.Orders;
using FluentAssertions;
using SharedKernel;

namespace Domain.UnitTests.Orders
{
    public class OrderTests
    {
        [Fact]
        public void Add_Should_Add_LineItems_To_The_Order()
        {
            //Arrange
            var order = Order.Create();
            var productOneId = new ProductId(Guid.NewGuid());
            var productOneMoney = new Money("LKR", 200);
            var productTwoId = new ProductId(Guid.NewGuid());
            var productTwoMoney = new Money("LKR", 250);

            //Act
            order.Add(productOneId, productOneMoney);
            order.Add(productTwoId, productTwoMoney);

            //Asert
            order.LineItems.Should().NotBeEmpty()
                .And.HaveCount(2)
                .And.ContainItemsAssignableTo<LineItem>();
        }
    }
}
