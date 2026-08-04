
using Ecommerce.Cart.Modules.Application.Features.GetCart;
using Ecommerce.Cart.Modules.Application.Features.Responses;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Ecommerce.Cart.Modules.Presentation.Carts;

internal sealed class GetCartEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/carts/{customerId:guid}", async (
            Guid customerId,
            ISender sender) =>
        {
            var result = await sender.Send(new GetCartQuery(customerId));

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(result.Error);
        })
        .WithTags(Tags.Carts)
        .Produces<CartResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
