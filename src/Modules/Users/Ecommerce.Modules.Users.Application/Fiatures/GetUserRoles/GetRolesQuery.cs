

using Ecommerce.Modules.Users.Contracts.Dto;

namespace Ecommerce.Modules.Users.Application.Fiatures.GetUserRoles;

public sealed record GetRolesQuery(Guid UserId) : IQuery<IReadOnlyCollection<RoleResponse>>;

