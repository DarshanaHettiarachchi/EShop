using Application.Orders.Create;
using Application.Orders.GetOrderSummary;
using Application.Orders.RemoveLineItem;
using Carter;
using Domain.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web.API.Endpoints;

public class Orders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("orders", async (ISender sender) =>
        {
            var command = new CreateOrderCommand();

            await sender.Send(command);

            return Results.Ok();
        });

        app.MapPost("orders/add-product", async ([FromBody] Guid id, ISender sender) =>
        {
            var command = new AddProductCommand(new ProductId(id));

            await sender.Send(command);

            return Results.Ok();
        });

        app.MapDelete("orders/{id}/line-items/{lineItemId}", async (Guid id, Guid lineItemId, ISender sender) =>
        {
            var command = new RemoveLineItemCommand(new OrderId(id), new LineItemId(lineItemId));

            await sender.Send(command);

            return Results.Ok();
        });

        app.MapGet("orders/{id}/summary", async (Guid id, ISender sender) =>
        {
            var query = new GetOrderSummaryQuery(id);

            return Results.Ok(await sender.Send(query));
        });
    }
}
