

using Ecommerce.Inventory.Modules.Application.Features.CommitReservation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Inventory.Modules.Presentation.Inventory;

internal sealed class CommitReservationEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/inventories/{inventoryItemId:guid}/commit", async (
            Guid inventoryItemId,

            CommitReservationRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CommitReservationCommand(
                inventoryItemId,
                request.Quantity);
            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? Results.NoContent()
                : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.Inventory)
        .WithName("CommitReservation")
         .Produces(StatusCodes.Status204NoContent)
         .Produces(StatusCodes.Status400BadRequest); 
    }
}
public sealed record CommitReservationRequest(int Quantity);
