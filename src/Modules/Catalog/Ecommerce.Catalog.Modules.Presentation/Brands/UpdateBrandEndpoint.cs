using Ecommerce.Catalog.Modules.Application.Features.Categories.UpdateCategory;


namespace Ecommerce.Catalog.Modules.Presentation.Brands;

internal sealed class UpdateBrandEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPut("/brands/{id:guid}", async (
            Guid id,
            UpdateBrandRequest request,
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
        .WithTags(Tags.Brands);
    }
}

internal sealed class UpdateBrandRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}