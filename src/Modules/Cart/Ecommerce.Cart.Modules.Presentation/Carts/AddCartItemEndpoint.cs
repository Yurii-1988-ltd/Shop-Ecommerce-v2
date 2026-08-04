

using Ecommerce.Cart.Modules.Application.Features.AddCartItem;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Ecommerce.Cart.Modules.Presentation.Carts;

internal sealed class AddCartItemEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/carts/{customerId:guid}/items", async (
            Guid customerId,
            AddCartItemRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new AddCartItemCommand(
              customerId,
               request.ProductId,
               request.Name,
               request.Price,
               request.Currency,
               request.Quantity
               );

            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? Results.NoContent()
                : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.Carts)
        .WithName("AddCartItem");
    }
}
internal sealed record AddCartItemRequest(

  
    Guid ProductId,
    string Name,
    decimal Price,
    string Currency,
    int Quantity);
