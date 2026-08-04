

using Ecommerce.Catalog.Modules.Application.Features.Brands.GetBrand;
using Ecommerce.Catalog.Modules.Application.Features.Brands.RemoveBrand;


namespace Ecommerce.Catalog.Modules.Presentation.Brands;

internal sealed class RemoveBrandEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapDelete("/brands/{id:guid}", async (
            Guid id,
            ISender sender) =>
        {
            var result = await sender.Send(new RemoveBrandCommand(id));

            return result.IsSuccess
                ? Results.NoContent()
                : Results.NotFound(result.Error);
        })
        .WithTags(Tags.Brands)
        .Produces<BrandResponse>(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}
