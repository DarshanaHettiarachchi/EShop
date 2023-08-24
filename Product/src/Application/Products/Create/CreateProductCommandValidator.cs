using Domain.Products;
using FluentValidation;

namespace Application.Products.Create
{
    public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator(IProductRepository productRepository)
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Product Name cannot be empty");

            RuleFor(p => p.Sku).MustAsync(async (sku, _) =>
            {
                return await productRepository.IsSKUUnique(Sku.Create(sku));
            }).WithMessage("SKU must be unique");
        }
    }
}
