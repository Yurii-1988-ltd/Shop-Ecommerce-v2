
using Ecommerce.Cart.Modules.Application.Features.RemoveCartItem;
using Ecommerce.Cart.Modules.Application.Features.Responses;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Ecommerce.Cart.Modules.Presentation.Carts;

internal sealed class RemoveCartItemEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapDelete("/carts/{customerId:guid}/{productId:guid}/items", async (
            Guid customerId,
            Guid productId,

            ISender sender) =>
        {
            var result = await sender.Send(new RemoveCartItemCommand(customerId,productId));

            return result.IsSuccess
                ? Results.NoContent()
                : Results.NotFound(result.Error);
        })
        .WithTags(Tags.Carts)
        .WithName("RemoveCartItem")
        .Produces<CartResponse>(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }

}
