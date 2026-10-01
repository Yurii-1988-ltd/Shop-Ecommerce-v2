


using Ecommerce.Localization.Modules.Presentation.Endpoints;
using Ecommerce.Localization.Modules.Presentation.Localizations;
using Ecommerce.Localization.Modules.Presentation.Translations;

namespace Ecommerce.Localization.Modules.Presentation;

public sealed class LocalizationModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        new GetTranslationsEndpoint().MapEndpoints(app);
        new CreateTranslationEndpoint().MapEndpoints(app);
        new UpdateTranslationEndpoint().MapEndpoints(app);
        new DeleteTranslationEndpoint().MapEndpoints(app);

        //languages
        new CreateLanguageEndpoint().MapEndpoints(app);
        new UpdateLanguageEndpoint().MapEndpoints(app);
        new DeleteLanguageEndpoint().MapEndpoints(app);
        new GetLanguageEndpoint().MapEndpoints(app);
        new GetLanguagesEndpoint().MapEndpoints(app);
        new EnableLanguageEndpoint().MapEndpoints(app);
        new DisableLanguageEndpoint().MapEndpoints(app);
        new TestLocalizationEndpoint().MapEndpoints(app);
    }

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddLocalizationModule(config);
    }
}
