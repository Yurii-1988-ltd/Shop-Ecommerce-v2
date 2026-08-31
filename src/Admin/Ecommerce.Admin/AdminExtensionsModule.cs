using Microsoft.AspNetCore.Authentication.Cookies;

namespace Ecommerce.Admin;

public static class AdminExtensionsModule
{
    public static IServiceCollection AddAdminModule(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "Ecommerce.Admin";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.LoginPath = "/login";
                options.AccessDeniedPath = "/access-denied";
            });
        return services;
    }



}
