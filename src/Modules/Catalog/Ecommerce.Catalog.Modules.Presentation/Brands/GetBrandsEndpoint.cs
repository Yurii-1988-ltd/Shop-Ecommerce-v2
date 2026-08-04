

using Ecommerce.Catalog.Modules.Application.Features.Brands.GetBrands;
using Ecommerce.Catalog.Modules.Application.Features.Categories.GetCategories;

namespace Ecommerce.Catalog.Modules.Presentation.Brands;

internal sealed class GetBrandsEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/brands", async (
        int page,
        int pageSize,
        ISender sender) =>
        {
            var result = await sender.Send(new GetBrandsQuery(page, pageSize));

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(result.Value);
        }).WithTags(Tags.Brands);
    }
}

