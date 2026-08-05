using Ecommerce.Application.Pagination;
using Ecommerce.Order.Modules.Application.Features.GetOrders;
using Ecommerce.Order.Modules.Application.Features.Responses;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Ecommerce.Order.Modules.Presentation.Orders;

internal sealed class GetOrdersEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async (
            [AsParameters] GetOrdersQuery query,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(query, cancellationToken);

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.Orders)
        .WithName("GetOrders")
        .Produces<PagedResult<OrderListResponse>>(StatusCodes.Status200OK)
        .Produces<ProblemDetails>(StatusCodes.Status400BadRequest);
    }
}