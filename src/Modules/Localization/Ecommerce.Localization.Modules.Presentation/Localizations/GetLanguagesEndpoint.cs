using Ecommerce.Localization.Modules.Application.Features.Language.GetLanguages;

namespace Ecommerce.Localization.Modules.Presentation.Localizations;

internal sealed class GetLanguagesEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/languages",
            async (
                ISender sender,
                int page = 1,
                int pageSize = 20,
                string? cultureCode = null,
                string? name = null,
                string? nativeName = null,
                bool? isDefault = null) =>
            {
                var result = await sender.Send(
                    new GetLanguagesQuery(
                        cultureCode,
                        name,
                        nativeName,
                        isDefault,
                        page,
                        pageSize));

                if (result.IsFailure)
                    return Results.BadRequest(result.Error);

                return Results.Ok(result.Value);
            })
            .WithTags(Tags.Languages);
    }
}