
using Ecommerce.Catalog.Modules.Application.Features.Categories.CreateCategory;

namespace Ecommerce.Catalog.Modules.Presentation.Categories;

internal sealed class CreateCategoryEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/categories", async (
            CreateCategoryRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateCategoryCommand(
                request.Name,
                request.Description
               );

            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? Results.Created($"/category/{result.Value}", result.Value)
                : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.Categories);
    }
}
public sealed class CreateCategoryRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
