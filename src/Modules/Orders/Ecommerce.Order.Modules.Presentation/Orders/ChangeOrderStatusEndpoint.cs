using Ecommerce.Order.Modules.Application.Features.ChangeStatus;
using Ecommerce.Order.Modules.Domain.Enums;
using System.Text.Json.Serialization;

namespace Ecommerce.Order.Modules.Presentation.Orders;

internal sealed class ChangeOrderStatusEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/orders/{orderId:guid}/status", async (
            Guid orderId,
           ChangeOrderStatusRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new ChangeOrderStatusCommand(
                orderId,
               request.Status

               );

            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? Results.NoContent()
                : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.Orders)
        .WithName("ChangeOrderStatus");
    }
    //[JsonConverter(typeof(JsonStringEnumConverter))]
 
}
public sealed record ChangeOrderStatusRequest(
    [property: JsonConverter(typeof(JsonStringEnumConverter))]
    OrderStatus Status
);
