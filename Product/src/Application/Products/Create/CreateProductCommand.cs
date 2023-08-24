using Application.Common.Interfaces.Messaging;

namespace Application.Products.Create;

public record CreateProductCommand(
    string Name,
    string Sku,
    string Currency,
    decimal Amount) : ICommand;
