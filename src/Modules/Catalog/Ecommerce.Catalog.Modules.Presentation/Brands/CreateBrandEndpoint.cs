

namespace Ecommerce.Catalog.Modules.Presentation.Brands;

internal sealed class CreateBrandEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/brands", async (
            CreateBrandRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateBrandCommand(
                request.Name,
                request.Description
               );

            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? Results.Created($"/brands/{result.Value}", result.Value)
                : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.Brands);
    }
}
public sealed class CreateBrandRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
