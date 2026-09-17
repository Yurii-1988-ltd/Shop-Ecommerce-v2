// DTO без поля Code
using Ecommerce.Cart.Modules.Application.Features.RemoveCart;

namespace Ecommerce.Cart.Modules.Presentation.Carts;

internal sealed class RemoveCartEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/admin/carts/{customerId:guid}", async (
            Guid customerId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new RemoveCartCommand(customerId),
                cancellationToken);

            return result.IsSuccess
                ? Results.NoContent()
                : Results.NotFound(new { error = result.Error.Code, description = result.Error.Description });
        })
        .WithName("RemoveCart")
        .WithTags(Tags.Carts);
    }
}