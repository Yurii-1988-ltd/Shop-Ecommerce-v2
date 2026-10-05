

using Ecommerce.Storefront.ApiClients.Identity.Contracts;

namespace Ecommerce.Storefront.ApiClients.Identity;

public sealed class IdentityApiClient(HttpClient httpClient) : IIdentityApiClient
{
    public async Task<UserProfileResponse?> GetProfileAsync(CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<UserProfileResponse>("/users/me",
            cancellationToken);

    }

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

    public async Task<AuthenticationResponse?> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/auth/register", request, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<AuthenticationResponse>(cancellationToken);

    }

    public async Task<bool> UpdateProfileAsync(
      UpdateProfileRequest request,
      CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync(
            "/users/me",
            request,
            cancellationToken);

        var content = await response.Content.ReadAsStringAsync(
            cancellationToken);


        return response.IsSuccessStatusCode;
    }
}

    

