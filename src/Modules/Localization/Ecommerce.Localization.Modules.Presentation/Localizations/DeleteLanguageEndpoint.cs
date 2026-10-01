using Ecommerce.Localization.Modules.Application.Features.Language.DeleteLanguage;


namespace Ecommerce.Localization.Modules.Presentation.Localizations;

internal sealed class DeleteLanguageEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapDelete(
            "/languages/{id:guid}",
            async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new DeleteLanguageCommand(id),
                    cancellationToken);

                if (result.IsFailure)
                    return Results.BadRequest(result.Error);

                return Results.NoContent();
            })
            .WithTags(Tags.Languages);
    }
}