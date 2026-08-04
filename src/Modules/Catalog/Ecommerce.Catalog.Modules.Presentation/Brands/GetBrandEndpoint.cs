

using Ecommerce.Catalog.Modules.Application.Features.Brands.GetBrand;


namespace Ecommerce.Catalog.Modules.Presentation.Brands;

internal sealed class GetBrandEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/brands/{id:guid}", async (
            Guid id,
            ISender sender) =>
        {
            var result = await sender.Send(new GetBrandQuery(id));

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(result.Error);
        })
        .WithTags(Tags.Brands)
        .Produces<BrandResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}