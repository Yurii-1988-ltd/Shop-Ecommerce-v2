
using Ecommerce.Catalog.Modules.Application.Features.Products.GetProductWithDetails;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Ecommerce.Catalog.Modules.Presentation.Products;

internal sealed class GetProductDetailsEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:guid}/details",
            async (Guid id,
                   ISender sender,
                   CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new GetProductsWithDetailsQuery(id),
                    cancellationToken);

                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : Results.NotFound(result.Error);
            })
        .WithTags(Tags.Products);
    }
}