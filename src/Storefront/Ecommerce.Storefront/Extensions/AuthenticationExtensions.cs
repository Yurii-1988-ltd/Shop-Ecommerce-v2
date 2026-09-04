using Microsoft.AspNetCore.Authentication.Cookies;

namespace Ecommerce.Storefront.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddStorefrontAuthentication(this IServiceCollection services)
    {
        services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/access-denied";
    });

        services.AddAuthorization();
        return services;
    }
}
