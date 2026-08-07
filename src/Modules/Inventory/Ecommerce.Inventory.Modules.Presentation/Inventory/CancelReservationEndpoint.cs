using Ecommerce.Inventory.Modules.Application.Features.CancelReservation;

namespace Ecommerce.Inventory.Modules.Presentation.Inventory;

internal sealed class CancelReservationEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/inventories/{inventoryItemId:guid}/cancel", async (
            Guid inventoryItemId,
    
            CancelReservationRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
        var command = new CancelReservationCommand(
            inventoryItemId,
            request.Quantity);
            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? Results.NoContent()
                : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.Inventory)
        .WithName("CancelReservation")
         .Produces(StatusCodes.Status204NoContent)
         .Produces(StatusCodes.Status400BadRequest); ;
    }
}
public sealed record CancelReservationRequest(
    int Quantity);
