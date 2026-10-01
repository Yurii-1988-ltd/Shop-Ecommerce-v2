using Ecommerce.Localization.Modules.Application.Features.Language.DisableLanguage;


namespace Ecommerce.Localization.Modules.Presentation.Localizations;

internal sealed class DisableLanguageEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPatch(
            "/languages/{id:guid}/disable",
            async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new DisableLanguageCommand(id),
                    cancellationToken);

                if (result.IsFailure)
                    return Results.BadRequest(result.Error);

                return Results.NoContent();
            })
            .WithTags(Tags.Languages);
    }
}