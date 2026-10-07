using Ecommerce.Shared.Contracts.Orders;
internal sealed class GetMyOrdersEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/my", async (
            [AsParameters] GetMyOrdersRequest request,
            [FromServices] ICurrentUser currentUser,
            [FromServices] IQueryHandler<
                GetMyOrdersQuery,
                PagedResult<OrderListResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            if (!currentUser.IsAuthenticated)
                return Results.Unauthorized();

            var query = new GetMyOrdersQuery(
                currentUser.UserId,
                request.Page,
                request.PageSize);

            var result = await handler.Handle(
                query,
                cancellationToken);

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Error);
        });
    }
}

public sealed record GetMyOrdersRequest(
    int Page = 1,
    int PageSize = 20);