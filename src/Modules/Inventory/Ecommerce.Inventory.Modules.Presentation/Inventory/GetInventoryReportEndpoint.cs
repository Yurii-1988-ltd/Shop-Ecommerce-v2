using Ecommerce.Inventory.Modules.Application.Features.GetInventoryReport;
using Ecommerce.Inventory.Modules.Application.Responses;
using MediatR;

namespace Ecommerce.Inventory.Modules.Presentation.Inventory;

internal sealed class GetInventoryReportEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/inventories/report", async (
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetInventoryReportQuery();

            var result = await sender.Send(
                query,
                cancellationToken);

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.Inventory)
        .WithName("GetInventoryReport")
        .Produces<IReadOnlyList<InventoryReportItem>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}