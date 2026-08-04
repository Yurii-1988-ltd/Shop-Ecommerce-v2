

using Ecommerce.Catalog.Modules.Application.Features.Categories.GetCategory;
using Ecommerce.Catalog.Modules.Application.Features.Categories.RemoveCategory;

namespace Ecommerce.Catalog.Modules.Presentation.Categories;

internal sealed class RemoveCategoryEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapDelete("/categories/{id:guid}", async (
            Guid id,
            ISender sender) =>
        {
            var result = await sender.Send(new RemoveCategoryCommand(id));

            return result.IsSuccess
                ? Results.NoContent()
                : Results.NotFound(result.Error);
        })
        .WithTags(Tags.Categories)
        .Produces<CategoryResponse>(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}
