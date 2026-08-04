using Ecommerce.Catalog.Modules.Application.Features.Images.RemoveProductImage;
using Ecommerce.Catalog.Modules.Application.Features.Products.Responses;
using Ecommerce.Catalog.Modules.Presentation.Products;

namespace Ecommerce.Catalog.Modules.Presentation.Images;

internal sealed class RemoveImageEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{productId:guid}/images/{imageId:guid}", async (
                Guid productId,
                Guid imageId,
                ISender sender) =>
            {
                var result = await sender.Send(new RemoveProductImageCommand(productId, imageId));

                return result.IsSuccess
                    ? Results.NoContent()
                    : Results.NotFound(result.Error);
            })
            .WithTags(Tags.Images)
            .Produces<ProductImageResponse>(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
    
}