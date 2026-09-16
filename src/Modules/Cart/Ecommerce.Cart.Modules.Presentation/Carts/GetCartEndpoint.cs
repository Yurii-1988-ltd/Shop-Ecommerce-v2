using Ecommerce.Cart.Modules.Application.Features.GetCart;
using Ecommerce.Cart.Modules.Application.Features.Responses;
using Ecommerce.Cart.Modules.Presentation;

internal sealed class GetCartEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/carts", async (
            Guid? customerId,
            Guid? guestId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new GetCartQuery(customerId, guestId),
                cancellationToken);

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(result.Error);
        })
        .WithTags(Tags.Carts)
        .Produces<CartResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}