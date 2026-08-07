
using Ecommerce.Inventory.Modules.Application.Features.ReplenishStock;

namespace Ecommerce.Inventory.Modules.Presentation.Inventory;

internal sealed class ReplenishStockEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/inventories/{inventoryItemId:guid}/replenish", async (
            Guid inventoryItemId,
            ReplenishStockRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new ReplenishStockCommand(inventoryItemId, request.Quantity);
            var result = await sender.Send(command, cancellationToken);
            return result.IsSuccess
                ? Results.NoContent()
                : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.Inventory)
        .WithName("ReplenishStock")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
public sealed record ReplenishStockRequest(int Quantity);
