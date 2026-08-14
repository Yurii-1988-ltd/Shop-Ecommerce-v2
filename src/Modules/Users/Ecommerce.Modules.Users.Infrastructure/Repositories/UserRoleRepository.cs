

namespace Ecommerce.Modules.Users.Infrastructure.Repositories;

internal sealed class UserRoleRepository : IUserRoleRepository
{
    public void Add(UserRole userRole)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistAsync(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Role>> GetRolesAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result> RemoveAsync(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
