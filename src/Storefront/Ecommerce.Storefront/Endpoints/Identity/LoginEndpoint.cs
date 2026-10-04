using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Storefront.Endpoints.Identity;

public sealed class LoginEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async (
                [FromForm] LoginRequest request,
                IIdentityApiClient identityApiClient,
                IStorefrontAuthenticationService authenticationService,
                CancellationToken cancellationToken) =>
        {
            var authentication = await identityApiClient.LoginAsync(
                request,
                cancellationToken);

            if (authentication is null)
            {
                return Results.Redirect(
                    "/auth/login?error=invalid_credentials");
            }

            await authenticationService.SignInAsync(
                authentication,
                cancellationToken);

            return Results.Redirect("/");
        })
            .DisableAntiforgery();
    }
}