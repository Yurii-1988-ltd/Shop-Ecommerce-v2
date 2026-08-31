using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Ecommerce.Admin.Authentication;

internal static class AuthenticationPrincipalFactory
{
    public static ClaimsPrincipal Create(string accessToken)
    {
        var jwt = new JwtSecurityTokenHandler()
            .ReadJwtToken(accessToken);

        var identity = new ClaimsIdentity(
            jwt.Claims,
            CookieAuthenticationDefaults.AuthenticationScheme,
            ClaimTypes.Name,
            ClaimTypes.Role);

        return new ClaimsPrincipal(identity);
    }
}