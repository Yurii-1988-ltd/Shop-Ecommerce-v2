using Ecommerce.Catalog.Modules.Application.Features.Images.AddImage;
using Ecommerce.Catalog.Modules.Presentation;
using Ecommerce.Catalog.Modules.Presentation.Images;

internal sealed class AddProductImageEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "/products/{productId:guid}/images",
                async (
                    Guid productId,
                    AddProductImageRequest request,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(
                        new AddProductImageCommand(
                            productId,
                            request.StorageKey,
                            request.AltText),
                        cancellationToken);

                    return result.IsSuccess
                        ? Results.NoContent()
                        : Results.NotFound(result.Error);
                })
           
            .WithTags(Tags.Images)
            .Produces(StatusCodes.Status204NoContent)
          .Produces(StatusCodes.Status404NotFound)
           ;
           
    }
}