using Ecommerce.Catalog.Modules.Application.Features.Images.SetPrimaryImage;

namespace Ecommerce.Catalog.Modules.Presentation.Images;

internal sealed class SetPrimaryImageEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "/products/{productId:guid}/images/{imageId:guid}/primary",
            async (
                Guid productId,
                Guid imageId,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new SetPrimaryImageCommand(productId, imageId),
                    cancellationToken);

                return result.IsSuccess
                    ? Results.NoContent()
                    : Results.NotFound(result.Error);
            }).WithTags(Tags.Images);
    }
    
}