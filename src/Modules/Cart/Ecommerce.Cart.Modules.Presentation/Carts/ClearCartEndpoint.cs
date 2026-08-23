

using Ecommerce.Cart.Modules.Application.Features.ClearCart;
using Ecommerce.Cart.Modules.Application.Features.Responses;

namespace Ecommerce.Cart.Modules.Presentation.Carts
{
    internal sealed class ClearCartEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("/carts/{customerId:guid}/items", async (
                Guid customerId,
                ISender sender) =>
            {
                var result = await sender.Send(new ClearCartCommand(customerId));

                return result.IsSuccess
                    ? Results.NoContent()
                    : Results.NotFound(result.Error);
            })
            .WithTags(Tags.Carts)
            .WithName("ClearCart")
            .Produces<CartResponse>(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
        }
    }
}
