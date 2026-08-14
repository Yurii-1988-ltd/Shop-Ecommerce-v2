

using Ecommerce.Modules.Users.Contracts.Abstractions;
using Ecommerce.Modules.Users.Contracts.Dto;

namespace Ecommerce.Modules.Users.Application.Fiatures.GetRoles;

internal sealed class GetRolesQueryHandler(IRoleService roleService) : IQueryHandler<GetRolesQuery, IReadOnlyList<RoleResponse>>
{
    public async Task<Result<IReadOnlyList<RoleResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {

        var roles = await roleService.GetAllAsync(cancellationToken);
        return roles;
    
    }
}
