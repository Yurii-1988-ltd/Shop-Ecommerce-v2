using Ecommerce.Cart.Modules.Application.Features.RemoveCart;

namespace Ecommerce.Cart.Modules.Presentation.Carts;

internal sealed class RemoveCartEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/carts/{customerId:guid}", async (
     Guid customerId,
     ISender sender,
     CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new RemoveCartCommand(customerId),
                cancellationToken);

            return result.IsSuccess
                ? Results.NoContent()
                : Results.NotFound(result.Error);
        })
            .WithTags(Tags.Carts);

    }
}