using Ecommerce.Localization.Modules.Application.Features.Language.EnableLanguage;

namespace Ecommerce.Localization.Modules.Presentation.Localizations;

internal sealed class EnableLanguageEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPatch(
            "/languages/{id:guid}/enable",
            async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new EnableLanguageCommand(id),
                    cancellationToken);

                if (result.IsFailure)
                    return Results.BadRequest(result.Error);

                return Results.NoContent();
            })
            .WithTags(Tags.Languages);
    }
}