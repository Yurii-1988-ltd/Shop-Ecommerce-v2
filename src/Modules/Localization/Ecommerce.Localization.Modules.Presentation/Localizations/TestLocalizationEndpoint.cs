using Ecommerce.Localization.Abstractions.Abstractions;

namespace Ecommerce.Localization.Modules.Presentation.Endpoints;

internal sealed class TestLocalizationEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/localization/test",
            async (
                ICurrentCulture currentCulture,
                ILocalizationService localization,
                CancellationToken cancellationToken) =>
            {
                var culture = currentCulture.Culture;

                var value = await localization.GetAsync(
                    "Product.NotFound",
                    "Catalog",
                    culture,
                    cancellationToken);

                return Results.Ok(new
                {
                    Culture = culture.Name,
                    Value = value
                });
            })
            .WithTags("Localization");
    }
}