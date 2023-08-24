using Application.Models;

namespace Application.Common.Interfaces
{
    public interface IOrderedProductService
    {
        Task<OrderedProduct?> GetOrderedProduct(Guid id);
    }
}
