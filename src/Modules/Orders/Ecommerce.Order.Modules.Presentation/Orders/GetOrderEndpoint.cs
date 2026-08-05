

using Ecommerce.Order.Modules.Application.Features.GetOrder;
using Ecommerce.Order.Modules.Application.Features.Responses;

namespace Ecommerce.Order.Modules.Presentation.Orders;

internal sealed class GetOrderEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{OrderId:guid}", async (
            Guid OrderId, ISender sender) =>
        {
            var result = await sender.Send(new GetOrderQuery(OrderId));
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(result.Error);
        })
            .WithTags(Tags.Orders)
            .Produces<OrderResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }
}
