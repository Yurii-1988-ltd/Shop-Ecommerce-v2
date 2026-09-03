using Ecommerce.Catalog.Modules.Application.Features.Products.GetProducts;

namespace Ecommerce.Catalog.Modules.Presentation.Products;

internal sealed class GetProductsEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (
        ISender sender,
        int page = 1,
        int pageSize = 20,
        string? search = null) =>
        {
            var result = await sender.Send(new GetProductsQuery(page, pageSize, search));

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(result.Value);
        })
.WithTags(Tags.Products);

    }
}
