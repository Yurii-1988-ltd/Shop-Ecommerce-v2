using Ecommerce.Admin.ApiClients.Users.Contracts;
using Ecommerce.Admin.ApiClients.Users.Responses;
using Ecommerce.Admin.Contracts;


namespace Ecommerce.Admin.ApiClients.Users.Api
{
    public interface IUserApiClient
    {
       Task AssignRoleAsync(Guid userId, Guid RoleId, CancellationToken cancellationToken = default);
       
        Task<PagedResult<UserResponse>> GetAllAsync(
     int page,
     int pageSize,
     CancellationToken cancellationToken = default);
        Task<Guid> CreateAsync(UserRequest userRequest, CancellationToken cancellationToken = default);
        Task<UserResponse> GetAsync(Guid userId, CancellationToken cancellationToken = default);
        Task UpdateAsync(
        Guid id,
        UpdateUserRequest request,
        CancellationToken cancellationToken = default);
        Task<IReadOnlyList<RoleResponse>> GetRolesAsync(
            Guid userId,
            CancellationToken cancellationToken = default);
       Task RemoveUserRoleAsync(Guid userId,Guid roleId, CancellationToken cancellationToken = default);
      
    }
}
