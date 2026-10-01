

using System.Globalization;

namespace Ecommerce.Localization.Modules.Presentation.Middleware;

public sealed class LocalizationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var culture = GetCulture(context);
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
        await next(context);
    }

    private static CultureInfo GetCulture(HttpContext context)
    {
        var culture = context.Request.Headers.AcceptLanguage
            .FirstOrDefault()
            ?.Split(',')
            .FirstOrDefault()
            ?.Split(';')
            .FirstOrDefault()
            ?.Trim();
        if(string.IsNullOrWhiteSpace(culture))
            return CultureInfo.InvariantCulture;
        try
        {
            return CultureInfo.GetCultureInfo(culture);
        }catch(CultureNotFoundException)
        {
            return CultureInfo.InvariantCulture;
        }
    }
}
