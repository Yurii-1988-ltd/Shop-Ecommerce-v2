using Ecommerce.Catalog.Modules.Application.Features.Images.UploadImage;
using Ecommerce.Catalog.Modules.Presentation;

internal sealed class UploadImageEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/images",
            async (
                IFormFile file,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                await using var stream = file.OpenReadStream();

                var result = await sender.Send(
                    new UploadImageCommand(
                        stream,
                        file.FileName,
                        file.ContentType,
                        file.Length),
                    cancellationToken);

                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : Results.BadRequest(result.Error);
            })
            .DisableAntiforgery()
            .WithTags(Tags.Images)
            .Produces<UploadImageRespnse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            ;
    }
}