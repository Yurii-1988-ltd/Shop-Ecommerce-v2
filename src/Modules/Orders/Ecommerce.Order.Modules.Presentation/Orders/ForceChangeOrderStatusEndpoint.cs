
using Ecommerce.Order.Modules.Application.Features.ForceChangeStatus;
using Ecommerce.Order.Modules.Domain.Enums;
using System.Text.Json.Serialization;

namespace Ecommerce.Order.Modules.Presentation.Orders;

internal sealed class ForceChangeOrderStatusEndpoint
{
    public  void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPatch("/orders/{orderId:guid}/force-change-status", async (Guid orderId,
            ForceChangeStatusRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new ForceChangeStatusCommand(
                orderId,
                request.Status,
                request.ChangedBy,
                request.Reason
            );
            var result = await sender.Send(command, cancellationToken);
            return result.IsSuccess
                ? Results.NoContent()
                : Results.Conflict(result.Error);

        })
            .WithTags(Tags.Orders)
            .WithName("ForceChangeOrderStatus");
    }
}
internal sealed record ForceChangeStatusRequest(
    [property: JsonConverter(typeof(JsonStringEnumConverter))]
    OrderStatus Status,
    Guid ChangedBy,
    string Reason
);
