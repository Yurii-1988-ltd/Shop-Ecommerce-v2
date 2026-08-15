

using Ecommerce.Modules.Users.Contracts.Abstractions;
using Ecommerce.Modules.Users.Contracts.Dto;

namespace Ecommerce.Modules.Users.Application.Fiatures.GetUserRoles;

internal sealed class GetRolesQueryHandler(IUserRoleService userRoleService) : IQueryHandler<GetRolesQuery, IReadOnlyCollection<RoleResponse>>
{
    public async Task<Result<IReadOnlyCollection<RoleResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        return await userRoleService.GetRolesAsync(
         request.UserId,
         cancellationToken);
    }
}
