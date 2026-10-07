using Ecommerce.Cart.Modules.Application.Features.ClearCart;

namespace Ecommerce.Cart.Modules.Presentation.Carts;

internal sealed class ClearGuestCartEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/carts/guests/{guestId:guid}", async (
            Guid guestId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new ClearCartCommand(guestId);
            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? Results.NoContent()
                : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.Carts)
        .WithName("ClearGuestCart");
    }
}