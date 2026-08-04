
using Ecommerce.Cart.Modules.Application.Features.CreateCart;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Ecommerce.Cart.Modules.Presentation.Carts;

internal sealed class CreateCartEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/carts", async (CreateCartCommand command,
                        ISender sender, CancellationToken canceletionToken) =>
        {
            var result = await sender.Send(command, canceletionToken);

            return result.IsSuccess
                ? Results.Created($"/carts/{result.Value}", result.Value)
                : Results.BadRequest(result.Error);
        }).WithTags(Tags.Carts)
            .WithName("CreateCart");
    }
}
