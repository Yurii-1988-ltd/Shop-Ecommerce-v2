using Ecommerce.Admin.ApiClients.Identity.Contracts;
using Ecommerce.Admin.ApiClients.Identity.Responses;

namespace Ecommerce.Admin.ApiClients.Identity.Api;

internal sealed class IdentityApiClient(HttpClient httpClient) : IIdentityApiClient
{
    public async Task<AuthenticationResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/auth/login",
            request, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<AuthenticationResponse>(
            cancellationToken)
            ?? throw new InvalidOperationException("Empty authentication response.");
    }
}
