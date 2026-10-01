using Ecommerce.Localization.Modules.Application.Features.Language.UpdateLanguage;


namespace Ecommerce.Localization.Modules.Presentation.Localizations;

internal sealed class UpdateLanguageEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "/languages/{id:guid}",
            async (Guid id,
                UpdateLanguageRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new UpdateLanguageCommand(
                        id,
                        request.CultureCode,
                        request.Name,
                        request.NativeName,
                        request.IsDefault),cancellationToken);
                if (result.IsFailure)
                    return Results.BadRequest(result.Error);
                return Results.NoContent();
            })
            .WithTags(Tags.Languages);
    }
}
public sealed record UpdateLanguageRequest(string CultureCode,
                                        string Name,
                                        string NativeName,
                                        bool IsDefault);