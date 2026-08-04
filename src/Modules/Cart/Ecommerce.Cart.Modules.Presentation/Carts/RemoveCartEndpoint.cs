using Ecommerce.Cart.Modules.Application.Features.RemoveCart;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Ecommerce.Cart.Modules.Presentation.Carts;

internal sealed class RemoveCartEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
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