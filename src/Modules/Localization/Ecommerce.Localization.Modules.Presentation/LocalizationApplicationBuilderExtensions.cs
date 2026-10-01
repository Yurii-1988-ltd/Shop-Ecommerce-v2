

using Ecommerce.Localization.Modules.Presentation.Middleware;

namespace Ecommerce.Localization.Modules.Presentation;

public static class LocalizationApplicationBuilderExtensions
{
    public static IApplicationBuilder UseLocalization(this IApplicationBuilder app)
    {
        app.UseMiddleware<LocalizationMiddleware>();
        return app;
    }
}
