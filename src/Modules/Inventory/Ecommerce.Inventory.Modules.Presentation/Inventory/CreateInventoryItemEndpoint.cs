using Ecommerce.Inventory.Modules.Application.Features.CreateInventoryItem;
using MediatR;

namespace Ecommerce.Inventory.Modules.Presentation.Inventory;

internal sealed class CreateInventoryItemEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/inventories", async (
            CreateInventoryItemRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateInventoryItemCommand(
                request.ProductId,
                request.SKU,
                request.Quantity,
                request.MinimumQuantity);

            var result = await sender.Send(
                command,
                cancellationToken);

            return result.IsSuccess
                ? Results.Created(
                    $"/inventories/{result.Value}",
                    result.Value)
                : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.Inventory)
        .WithName("CreateInventoryItem")
        .Produces<Guid>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);
    }
}

public sealed record CreateInventoryItemRequest(
    Guid ProductId,
    string SKU,
    int Quantity,
    int MinimumQuantity);