

using Ecommerce.Order.Modules.Application.Features.ChangeOrderItemQuantity;
using Ecommerce.Order.Modules.Application.Features.ChangeStatus;

namespace Ecommerce.Order.Modules.Presentation.Orders;

internal sealed class ChangeOrderItemQuantityEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/orders/{orderId:guid}/items/{orderItemId:guid}/quantity", async (
            Guid orderId,
            Guid orderItemId,
            ChangeOrderItemQuantityRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new ChangeOrderItemQuantityCommand(
                orderId,
                orderItemId,
                request.Quantity);

            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? Results.NoContent()
                : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.Orders)
        .WithName("ChangeOrderItemQuantity");
    }
}

public sealed record ChangeOrderItemQuantityRequest(
    int Quantity);