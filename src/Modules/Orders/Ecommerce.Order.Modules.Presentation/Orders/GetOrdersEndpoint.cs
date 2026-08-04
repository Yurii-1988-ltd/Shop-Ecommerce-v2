
namespace Ecommerce.Order.Modules.Presentation.Orders
{
    internal sealed class GetOrdersEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders", async (
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        ISender sender = default!) =>
            {
                var result = await sender.Send(new GetOrdersQuery(page, pageSize));

                if (result.IsFailure)
                    return Results.BadRequest(result.Error);

                return Results.Ok(result.Value);
            })
                .WithTags(Tags.Orders)
                .WithName("GetOrders");
        }
    }
}
