
using Ecommerce.Cart.Modules.Application.Features.GetCarts;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Cart.Modules.Presentation.Carts;

internal sealed class GetCartsEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/carts", async (
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10,
    ISender sender = default!) =>
        {
            var result = await sender.Send(new GetCartsQuery(page, pageSize));

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(result.Value);
        })
            .WithTags(Tags.Carts)
            .WithName("GetCarts");
    }
}
