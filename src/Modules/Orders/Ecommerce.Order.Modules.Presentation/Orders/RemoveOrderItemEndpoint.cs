using Ecommerce.Order.Modules.Application.Features.RemoveOrderItem;
using Ecommerce.Order.Modules.Presentation;

internal sealed class RemoveOrderItemEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(
            "/orders/{orderId:guid}/items/{orderItemId:guid}",
            async (
                Guid orderId,
                Guid orderItemId,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new RemoveOrderItemCommand(
                    orderId,
                    orderItemId);

                var result = await sender.Send(
                    command,
                    cancellationToken);

                return result.IsSuccess
                    ? Results.NoContent()
                    : Results.BadRequest(result.Error);
            })
            .WithTags(Tags.Orders)
            .WithName("RemoveOrderItem")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest);
    }
}