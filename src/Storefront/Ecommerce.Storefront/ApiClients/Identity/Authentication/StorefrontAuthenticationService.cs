using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Ecommerce.Storefront.ApiClients.Identity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Ecommerce.Storefront.ApiClients.Identity.Authentication;

internal sealed class StorefrontAuthenticationService(
    IHttpContextAccessor httpContextAccessor)
    : IStorefrontAuthenticationService
{
    public async Task SignInAsync(
        AuthenticationResponse authenticationResponse,
        CancellationToken cancellationToken = default)
    {
        var token = new JwtSecurityTokenHandler()
            .ReadJwtToken(authenticationResponse.AccessToken);

        var userId = token.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
            ?.Value;

        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new InvalidOperationException(
                "User ID claim is missing in the access token.");
        }

        var claims = token.Claims
            .Where(x =>
                x.Type == ClaimTypes.NameIdentifier ||
                x.Type == ClaimTypes.Email ||
                x.Type == ClaimTypes.Role)
            .ToList();

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);

        var httpContext = httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException(
                "HTTP context is unavailable.");

        await httpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);
    }
}