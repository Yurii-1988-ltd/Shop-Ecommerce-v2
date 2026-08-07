

namespace Ecommerce.Inventory.Modules.Presentation.Inventory;

internal sealed class ReserveStockEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/inventories/{inventoryItemId:guid}/reserve", async (
            Guid inventoryItemId,
            ReserveStockRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new ReserveStockCommand(inventoryItemId, request.Quantity);
            var result = await sender.Send(command, cancellationToken);
            return result.IsSuccess
                 ? Results.NoContent()
                 : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.Inventory)
        .WithName("ReserveStock")
         .Produces(StatusCodes.Status204NoContent)
         .Produces(StatusCodes.Status400BadRequest);
    }

}
public sealed record ReserveStockRequest(int Quantity);