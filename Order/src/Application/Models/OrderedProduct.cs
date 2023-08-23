namespace Application.Models
{
    public record OrderedProduct(
         Guid Id,
         string Name,
         string Currency,
         decimal Amount
    );
}
