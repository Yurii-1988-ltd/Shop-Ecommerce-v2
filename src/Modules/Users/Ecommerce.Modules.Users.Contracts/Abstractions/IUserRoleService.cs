
using Ecommerce.Domain.Domain;
using Ecommerce.Modules.Users.Contracts.Dto;

namespace Ecommerce.Modules.Users.Contracts.Abstractions;

public interface IUserRoleService
{
    Task<Result<Guid>> AssignAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default);
    Task<Result>RemoveAsync(Guid userId, Guid roleId, CancellationToken cancellationToken= default);
    Task<Result<IReadOnlyCollection<RoleResponse>>>GetRolesAsync(Guid userId, CancellationToken cancellationToken= default);

}
