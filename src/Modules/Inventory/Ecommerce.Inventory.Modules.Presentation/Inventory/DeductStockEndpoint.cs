

using Ecommerce.Inventory.Modules.Application.Features.DeductStock;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Inventory.Modules.Presentation.Inventory;

internal sealed class DeductStockEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/inventories/{inventoryItemId:guid}/deduct", async (
            Guid inventoryItemId,

            DeductStockRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new DeductStockCommand(
                inventoryItemId,
                request.Quantity);
            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? Results.NoContent()
                : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.Inventory)
        .WithName("DeductStock")
         .Produces(StatusCodes.Status204NoContent)
         .Produces(StatusCodes.Status400BadRequest);
    }
}
public sealed record DeductStockRequest(int Quantity);
