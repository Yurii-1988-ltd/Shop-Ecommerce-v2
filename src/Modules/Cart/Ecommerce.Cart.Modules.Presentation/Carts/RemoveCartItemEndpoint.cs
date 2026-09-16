
using Ecommerce.Cart.Modules.Application.Features.RemoveCartItem;
using Ecommerce.Cart.Modules.Application.Features.Responses;

namespace Ecommerce.Cart.Modules.Presentation.Carts;

internal sealed class RemoveCartItemEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/carts/{customerId:guid}/{productId:guid}/items", async (
            Guid? customerId,
            Guid? guestId,
            Guid productId,

            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new RemoveCartItemCommand(customerId, guestId, productId), cancellationToken);

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
