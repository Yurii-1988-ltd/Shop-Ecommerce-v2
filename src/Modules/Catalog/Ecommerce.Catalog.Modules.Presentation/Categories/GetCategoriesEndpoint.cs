
using Ecommerce.Catalog.Modules.Application.Features.Categories.GetCategories;


namespace Ecommerce.Catalog.Modules.Presentation.Categories;

internal sealed class GetCategoriesEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/categories", async (
        int page,
        int pageSize,
        ISender sender) =>
        {
            var result = await sender.Send(new GetCategoriesQuery(page, pageSize));

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(result.Value);
        }).WithTags(Tags.Categories);
    }
}
