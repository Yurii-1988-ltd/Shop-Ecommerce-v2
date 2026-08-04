using Ecommerce.Order.Modules.Application.Contracts;

using MediatR;

namespace Ecommerce.Order.Modules.Presentation.Orders;

internal sealed class CreateOrderEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (
            CreateOrderRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateOrderCommand(
                request.CustomerId,
                request.ShippingAddress,
                request.Items,
                request.Currency);

            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? Results.Created($"/orders/{result.Value}", result.Value)
                : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.Orders)
        .WithName("CreateOrder");
    }
}