using Ecommerce.Admin.ApiClients.Identity.Contracts;
using Ecommerce.Admin.ApiClients.Identity.Responses;
using System.Text.Json;

namespace Ecommerce.Admin.ApiClients.Identity.Api;

internal sealed class IdentityApiClient(HttpClient httpClient) : IIdentityApiClient
{
    public async Task<AuthenticationResponse?> LoginAsync(
     LoginRequest request,
     CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync(
            "/auth/login",
            request,
            cancellationToken);

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Identity API returned {(int)response.StatusCode}: {content}");
        }

        return JsonSerializer.Deserialize<AuthenticationResponse>(
            content,
            new JsonSerializerOptions(JsonSerializerDefaults.Web))
            ?? throw new InvalidOperationException(
                "Empty authentication response.");
    }
}
