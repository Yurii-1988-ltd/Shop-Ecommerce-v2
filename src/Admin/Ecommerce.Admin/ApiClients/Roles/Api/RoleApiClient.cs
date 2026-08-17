using Ecommerce.Admin.ApiClients.Users.Responses;

namespace Ecommerce.Admin.ApiClients.Roles.Api;

internal sealed class RoleApiClient(HttpClient httpClient) : IRoleApiClient
{
    private const string RoleUrl = "/roles";
    public async Task RemoveRoleAsync(Guid userId,Guid roleId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync(
           $"{RoleUrl}/{roleId}", cancellationToken);
        response.EnsureSuccessStatusCode();
    }
    public async Task<IReadOnlyList<RoleResponse>> GetAllRolesAsync(
  CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<IReadOnlyList<RoleResponse>>(
            RoleUrl,
            cancellationToken) ?? [];
    }
}
