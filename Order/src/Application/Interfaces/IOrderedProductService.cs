using Application.Models;

namespace Application.Interfaces
{
    public interface IOrderedProductService
    {
        Task<OrderedProduct> GetOrderedProduct(Guid id);
    }
}
