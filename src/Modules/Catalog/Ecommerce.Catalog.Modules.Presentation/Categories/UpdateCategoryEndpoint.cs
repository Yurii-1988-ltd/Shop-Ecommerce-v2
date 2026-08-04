using Ecommerce.Catalog.Modules.Application.Features.Categories.UpdateCategory;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Ecommerce.Catalog.Modules.Presentation.Categories;

internal sealed class UpdateCategoryEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPut("/categories/{id:guid}", async (
            Guid id,
            UpdateCategoryRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateBrandCommand(
                id,
                request.Name,
                request.Description
               );

            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? Results.NoContent()
                : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.Categories);
    }
}

internal sealed class UpdateCategoryRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}