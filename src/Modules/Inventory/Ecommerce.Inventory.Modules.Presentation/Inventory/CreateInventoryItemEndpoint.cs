using Ecommerce.Inventory.Modules.Application.Features.CreateInventoryItem;


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
                : ApiResults.Problem(result);
        })
        .WithTags(Tags.Inventory)
        .WithName("CreateInventoryItem")
        .Produces<Guid>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status409Conflict);
    }
}

public sealed record CreateInventoryItemRequest(
    Guid ProductId,
    string SKU,
    int Quantity,
    int MinimumQuantity);