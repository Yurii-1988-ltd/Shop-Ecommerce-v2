using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Admin.Authentication.Endpoints;

internal sealed class AdminLoginEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/account/login", HandleAsync);
     
    }

    private static async Task<IResult> HandleAsync(
        [FromForm] string email,
        [FromForm] string password,
        IIdentityApiClient identityApi,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var response = await identityApi.LoginAsync(
            new LoginRequest(email, password),
            cancellationToken);

        var principal = AuthenticationPrincipalFactory.Create(
            response.AccessToken);

        await httpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);

        return Results.Redirect("/");
    }
}