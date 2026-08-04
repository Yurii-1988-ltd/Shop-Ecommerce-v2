

using Ecommerce.Catalog.Modules.Application.Features.Products.GetProduct;
using Ecommerce.Catalog.Modules.Application.Features.Products.RemoveProduct;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Ecommerce.Catalog.Modules.Presentation.Products;

internal sealed class RemoveProductEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id:guid}", async (
            Guid id,
            ISender sender) =>
        {
            var result = await sender.Send(new RemoveProductCommand(id));

            return result.IsSuccess
                ? Results.NoContent()
                : Results.NotFound(result.Error);
        })
        .WithTags(Tags.Products)
        .Produces<ProductResponse>(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}
