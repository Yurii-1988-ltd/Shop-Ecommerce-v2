using Ecommerce.Catalog.Modules.Application.Features.Images.ChangeImageOrder;
using Ecommerce.Catalog.Modules.Application.Features.Products.Responses;

namespace Ecommerce.Catalog.Modules.Presentation.Images;

internal sealed class ChangeImageOrderEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "/products/{productId:guid}/images/{imageId:guid}/order",
            async (
                Guid productId,
                Guid imageId,
                ChangeImageOrderRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new ChangeImageOrderCommand(
                        productId,
                        imageId,
                        request.NewIndex),
                    cancellationToken);

                return result.IsSuccess
                    ? Results.NoContent()
                    : Results.NotFound(result.Error);
            }).WithTags(Tags.Images);
    }
    
}