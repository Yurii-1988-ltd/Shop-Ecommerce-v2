


using Ecommerce.Localization.Modules.Presentation.Translations;

namespace Ecommerce.Localization.Modules.Presentation
{
    public sealed class LocalizationModule : IModule
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            new GetTranslationsEndpoint().MapEndpoints(app);
            new CreateTranslationEndpoint().MapEndpoints(app);
            new UpdateTranslationEndpoint().MapEndpoints(app);
            new DeleteTranslationEndpoint().MapEndpoints(app);
        }

        public void RegisterServices(IServiceCollection services, IConfiguration config)
        {
            services.AddLocalizationModule(config);
        }
    }
}
