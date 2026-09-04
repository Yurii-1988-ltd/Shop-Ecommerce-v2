using Ecommerce.Storefront.ApiClients.Identity.Models;

namespace Ecommerce.Storefront.ApiClients.Identity;

public sealed class IdentityApiClient(HttpClient httpClient) : IIdentityApiClient
{
    public async Task<AuthenticationResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/auth/login", request, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<AuthenticationResponse>(cancellationToken);
    }

    public async Task<AuthenticationResponse?> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/auth/refresh", new { RefreshToken = refreshToken }, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<AuthenticationResponse>(cancellationToken);
    }
}

    

