using Ecommerce.Inventory.Modules.Application.Features.GetInventoryAvailability;
using Ecommerce.Inventory.Modules.Application.Features.Responses;

namespace Ecommerce.Inventory.Modules.Presentation.Inventory;

internal sealed class GetInventoryAvailabilityEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/inventories/product/{productId:guid}",
            async (
                Guid productId,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new GetInventoryAvailabilityQuery(productId),
                    cancellationToken);

                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : Results.NotFound(result.Error);
            })
        .WithTags(Tags.Inventory)
        .WithName("GetInventoryAvailability")
        .Produces<InventoryAvailabilityResponse>(
            StatusCodes.Status200OK)
        .Produces(
            StatusCodes.Status404NotFound);
    }
}
