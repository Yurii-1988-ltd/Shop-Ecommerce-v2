namespace Ecommerce.Localization.Modules.Presentation.Translations;

internal sealed class UpdateTranslationEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "/translations/{id:guid}",
            async (
                Guid id,
                UpdateTranslationRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new UpdateTranslationCommand(
                        id,
                        request.Value,
                        request.Description),cancellationToken);

                if (result.IsFailure)
                    return Results.BadRequest(result.Error);

                return Results.NoContent();
            })
            .WithTags(Tags.Translations);
    }
}

public sealed record UpdateTranslationRequest(
    string Value,
    string? Description);