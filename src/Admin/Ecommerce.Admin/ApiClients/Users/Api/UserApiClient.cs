using Ecommerce.Admin.ApiClients.Users.Contracts;
using Ecommerce.Admin.ApiClients.Users.Responses;
using System.Net;

namespace Ecommerce.Admin.ApiClients.Users.Api;

internal sealed class UserApiClient(HttpClient httpClient) : IUserApiClient
{
    private const string UserUrl = "/users";
    private const string RoleUrl = "/roles";

    public async Task AssignRoleAsync(
        Guid userId,
        Guid roleId,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync(
            $"{UserUrl}/{userId}/roles/{roleId}",
            null,
            cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task<Guid> CreateAsync(UserRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync(
            UserUrl,
            request,
            cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Guid>(cancellationToken);
    }

    public async Task<PagedResult<UserResponse>> GetAllAsync(
     int page,
     int pageSize,
     CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<PagedResult<UserResponse>>(
            $"{UserUrl}?page={page}&pageSize={pageSize}",
            cancellationToken) ?? new();
    }

    public async Task<UserResponse?> GetAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"{UserUrl}/{userId}", cancellationToken);
        if(response.StatusCode==HttpStatusCode.NotFound)
            return null;
        return await response.Content.ReadFromJsonAsync<UserResponse>(cancellationToken);
    }

    public async Task RemoveRoleAsync(Guid roleId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync(
           $"{RoleUrl}/{roleId}", cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateAsync(
     Guid id,
     UpdateUserRequest request,
     CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync(
            $"{UserUrl}/{id}",
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();
    }
}