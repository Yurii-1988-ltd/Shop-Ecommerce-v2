

namespace Ecommerce.Storefront.Endpoints.Identity;

public sealed class LogoutEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/logout", async (
            HttpContext httpContext) =>
        {
            await httpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return Results.Redirect("/");
        })
        .DisableAntiforgery();
    }
}