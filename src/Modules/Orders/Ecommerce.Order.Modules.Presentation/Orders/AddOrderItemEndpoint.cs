

using Ecommerce.Order.Modules.Application.Features.AddOrderItem;

namespace Ecommerce.Order.Modules.Presentation.Orders;

internal sealed class AddOrderItemEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders/{orderId:guid}/items", async (
            Guid orderId,
            AddOrderItemRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new AddOrderItemCommand(
                orderId,
               request.ProductId, 
               request.Quantity
               );

            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? Results.NoContent()
                : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.Orders)
        .WithName("AddOrderItem");
    }
}


internal sealed record AddOrderItemRequest(

    Guid ProductId,
    int Quantity);