using Ecommerce.Order.Modules.Application.Features.CancelOrder;


namespace Ecommerce.Order.Modules.Presentation.Orders;

internal class CancelOrderEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/orders/{orderId:guid}/cancel", async (
            Guid orderId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CancelOrderCommand(
                orderId);

            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? Results.NoContent()
                : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.Orders)
        .WithName("CancelOrder");
    }
}
