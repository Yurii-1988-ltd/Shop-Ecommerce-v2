using Ecommerce.Cart.Modules.Application.Features.ChangeCartItemQuantity;

namespace Ecommerce.Cart.Modules.Presentation.Carts;

internal sealed class ChangeCartItemQuantityEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "/carts/items/{productId:guid}",
            async (
                Guid productId,
                Guid? customerId,
                Guid? guestId,
                ChangeCartItemQuantityRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new ChangeCartItemQuantityCommand(
                        customerId,
                        guestId,
                        productId,
                        request.Quantity),
                    cancellationToken);

                return result.IsSuccess
                    ? Results.NoContent()
                    : Results.BadRequest(result.Error);
            })
            .WithTags(Tags.Carts)
            .WithName("ChangeCartItemQuantity")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);
    }
}

internal sealed record ChangeCartItemQuantityRequest(int Quantity);