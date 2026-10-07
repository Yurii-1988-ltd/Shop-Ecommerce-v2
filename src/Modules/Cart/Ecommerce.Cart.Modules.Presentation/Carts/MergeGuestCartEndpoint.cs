


using Ecommerce.Cart.Modules.Application.Features.MergeGuestCart;

namespace Ecommerce.Cart.Modules.Presentation.Endpoints;

internal sealed class MergeGuestCartEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/carts/merge", async (
            MergeGuestCartRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new MergeGuestCartCommand(
                    request.GuestId,
                    request.CustomerId),
                cancellationToken);

            return result.IsSuccess
                ? Results.NoContent()
                : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.Carts)
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
public sealed record MergeGuestCartRequest(Guid GuestId, Guid CustomerId);