

using Ecommerce.Modules.Users.Application.Mapper;
using Ecommerce.Modules.Users.Contracts.Dto;

namespace Ecommerce.Modules.Users.Infrastructure.Services;

internal sealed class RoleService(IRoleRepository repository) : IRoleService
{
    public async Task<Result<Guid>> CreateAsync(string name, CancellationToken cancellationToken = default)
    {
        var result = Role.Create(name);
        if(result.IsFailure)
        {
            return result.Error;
        }
        repository.Add(result.Value);
        return result.Value.Id;
       
    }

    public async Task<Result<IReadOnlyList<RoleResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var roles = await repository.GetAllAsync(cancellationToken);
        return roles.Select(x=>x.ToResponse())
            .ToList();
      
       
    }
    public async Task<Result<RoleResponse?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var role = await repository.GetByIdAsync(id, cancellationToken);
        if (role is null)
            return RolesErrors.NotFound(id);
         return role.ToResponse();
       
             
    }
}
