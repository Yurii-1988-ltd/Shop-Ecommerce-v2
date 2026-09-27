using Ecommerce.Localization.Modules.Application.Features.Translation.GetTranslations;


namespace Ecommerce.Localization.Modules.Presentation.Translations;

internal sealed class GetTranslationsEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/translations",
            async (
                ISender sender,
                int page = 1,
                int pageSize = 20,
                string? key = null,
                string? cultureCode = null,
                string? module = null) =>
            {
                var result = await sender.Send(
                    new GetTranslationsQuery(
                        key,
                        cultureCode,
                        module,
                        page,
                        pageSize));

                if (result.IsFailure)
                    return Results.BadRequest(result.Error);

                return Results.Ok(result.Value);
            })
            .WithTags(Tags.Translations);
    }
}