using Ecommerce.Admin.ApiClients.Users.Responses;

namespace Ecommerce.Admin.ApiClients.Roles.Api
{
    public interface IRoleApiClient
    {
        Task RemoveRoleAsync( Guid userId, Guid roleId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<RoleResponse>> GetAllRolesAsync(
                             CancellationToken cancellationToken = default);
    }
}
