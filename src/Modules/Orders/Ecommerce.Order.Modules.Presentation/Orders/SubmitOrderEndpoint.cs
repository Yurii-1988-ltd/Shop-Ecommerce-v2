
namespace Ecommerce.Order.Modules.Presentation.Orders;

internal sealed class SubmitOrderEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders/{orderId:guid}/submit", async (
            Guid orderId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new SubmitOrderCommand(
                orderId);

            var result = await sender.Send(command, cancellationToken);

            return result.Match(
                onSuccess: () => Results.Ok(),
                onFailure: error => Results.BadRequest(error));
               
        })
        .WithTags(Tags.Orders)
        .WithName("SubmitOrder");
    }
}
