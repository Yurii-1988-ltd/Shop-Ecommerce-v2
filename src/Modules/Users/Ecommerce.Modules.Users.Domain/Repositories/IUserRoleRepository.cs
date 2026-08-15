

using Ecommerce.Domain.Domain;
using Ecommerce.Modules.Users.Domain.Entities;

namespace Ecommerce.Modules.Users.Domain.Repositories;

public interface IUserRoleRepository
{
    void Add(UserRole userRole);
    Task<bool> ExistsAsync(Guid userId, Guid roleId,CancellationToken cancellationToken);
    Task<Result> RemoveAsync(Guid userId, Guid roleId, CancellationToken cancellationToken);
    Task<IReadOnlyList<Role>>GetRolesAsync(Guid userId, CancellationToken cancellationToken);

}
