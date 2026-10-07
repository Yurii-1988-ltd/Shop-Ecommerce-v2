using Ecommerce.Storefront.ApiClients.Carts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Storefront.Endpoints;

public sealed class LoginEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async (
                [FromForm] LoginRequest request,
                IIdentityApiClient identityApiClient,
                ICartMergeService cartMergeService,
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
            await cartMergeService.MergeGuestCartAsync(cancellationToken);

            return Results.Redirect("/");
        })
        .DisableAntiforgery();
    }
}