

namespace Ecommerce.Modules.Users.Infrastructure.Repositories;

internal sealed class UserRoleRepository(UserDbContext context) : IUserRoleRepository
{
    public void Add(UserRole userRole)
    {
      context.UserRoles.Add(userRole);
    }

    public async Task<bool> ExistsAsync(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        return await context.UserRoles
            .AnyAsync(x=>x.UserId==userId && x.RoleId==roleId,cancellationToken);
        
    }

    public async Task<IReadOnlyList<Role>> GetRolesAsync(Guid userId, CancellationToken cancellationToken)
    {
      return 
            await context.UserRoles.Where(x=>x.Id==userId)
            .Select(x=>x.Role)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
       
    }

    public async  Task<Result> RemoveAsync(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
       var userRoles = await context.UserRoles
            .FirstOrDefaultAsync(x=>x.UserId== userId && x.RoleId==roleId, cancellationToken);
        if (userRoles == null)
            return UserErrors.RoleAssignmentNotFound;
        context.UserRoles.Remove(userRoles);
        return Result.Success();
    }
}
