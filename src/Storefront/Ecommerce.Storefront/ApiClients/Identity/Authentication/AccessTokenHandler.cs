using System.Net.Http.Headers;

namespace Ecommerce.Storefront.ApiClients.Identity.Authentication;

internal sealed class AccessTokenHandler(
    IHttpContextAccessor httpContextAccessor)
    : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (request.RequestUri?.AbsolutePath is "/auth/login"
            or "/auth/refresh")
        {
            return await base.SendAsync(
                request,
                cancellationToken);
        }

        var accessToken = httpContextAccessor.HttpContext?
            .User
            .FindFirst(AuthenticationClaimTypes.AccessToken)
            ?.Value;

        if (!string.IsNullOrWhiteSpace(accessToken))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    accessToken);
        }

        return await base.SendAsync(
            request,
            cancellationToken);
    }
}