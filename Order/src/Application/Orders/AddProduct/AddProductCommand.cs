using MediatR;
using SharedKernel;

namespace Application.Orders.Create;

public record AddProductCommand(ProductId ProductId) : IRequest;
