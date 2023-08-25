using Domain.Primitives;
using SharedKernel;

namespace Domain.Products;

public class Product : Entity
{
    public Product(ProductId id, string name, Money price)
    {
        Id = id;
        Name = name;
        Price = price;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Product()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public ProductId Id { get; private set; }

    //Handle as a value Obj
    public string Name { get; private set; } = string.Empty;

    public Money Price { get; private set; }

    public void Update(string name, Money price)
    {
        Name = name;
        Price = price;
    }
}
