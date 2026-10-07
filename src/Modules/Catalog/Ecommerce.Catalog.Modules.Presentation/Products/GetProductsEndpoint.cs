
namespace Ecommerce.Catalog.Modules.Presentation.Products;

internal sealed class GetProductsEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (
            ISender sender,
            [AsParameters] GetProductsQuery query) =>
        {
            var result = await sender.Send(query);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(result.Value);
        })
        .WithTags(Tags.Products);
    }
}