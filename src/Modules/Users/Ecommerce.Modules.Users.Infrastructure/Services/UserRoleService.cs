
using Ecommerce.Modules.Users.Application.Mapper;
using Ecommerce.Modules.Users.Contracts.Dto;

namespace Ecommerce.Modules.Users.Infrastructure.Services;

internal sealed class UserRoleService(IRoleRepository roleRepository, 
    IUserRepository userRepository,IUserRoleRepository userRoleRepository) : IUserRoleService
{
    public async Task<Result<Guid>> AssignAsync(
    Guid userId,
    Guid roleId,
    CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(
            userId,
            cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        var role = await roleRepository.GetByIdAsync(
            roleId,
            cancellationToken);

        if (role is null)
        {
            return RolesErrors.NotFound(roleId);
        }

        var exists = await userRoleRepository.ExistsAsync(
            userId,
            roleId,
            cancellationToken);

        if (exists)
        {
            return UserErrors.RoleAlreadyAssigned;
        }

        var userRole = UserRole.Create(userId, roleId);

        userRoleRepository.Add(userRole);

        return userRole.Id;
    }


    public async Task<Result<IReadOnlyCollection<RoleResponse>>> GetRolesAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var userRole = await userRoleRepository.GetRolesAsync(userId, cancellationToken);
        if (userRole == null)
            return UserErrors.NotFound(userId);
        return userRole.Select(x => x.ToResponse())
            .ToList();
       
    }

    public async Task<Result> RemoveAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default)
    {
        var userRole = await userRoleRepository.RemoveAsync(userId, roleId, cancellationToken);
        if (userRole.IsFailure)
            return userRole;
        return Result.Success();
    }
}
