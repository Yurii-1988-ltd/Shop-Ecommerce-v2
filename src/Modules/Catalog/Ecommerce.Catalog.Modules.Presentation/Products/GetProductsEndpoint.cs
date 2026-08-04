using Ecommerce.Catalog.Modules.Application.Features.Products.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Ecommerce.Catalog.Modules.Presentation.Products;

internal sealed class GetProductsEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (
        ISender sender,
        int page = 1,
        int pageSize = 20) =>
        {
            var result = await sender.Send(new GetProductsQuery(page, pageSize));

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(result.Value);
        })
.WithTags(Tags.Products);

    }
}
