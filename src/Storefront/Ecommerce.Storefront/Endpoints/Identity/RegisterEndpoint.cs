using Ecommerce.Storefront.ApiClients.Carts.Services;
using Ecommerce.Storefront.ApiClients.Identity.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Storefront.Endpoints.Identity;

public sealed class RegisterEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/register", async (
                [FromForm] RegisterRequest request,
                IIdentityApiClient identityApiClient,
                ICartMergeService cartMergeService,
                IStorefrontAuthenticationService authenticationService,
                CancellationToken cancellationToken) =>
        {
            var authentication =
                await identityApiClient.RegisterAsync(
                    request,
                    cancellationToken);

            if (authentication is null)
            {
                return Results.Redirect(
                    "/auth/register?error=registration_failed");
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