

using Ecommerce.Modules.Users.Contracts.Dto;

namespace Ecommerce.Modules.Users.Application.Fiatures.GetRoles;

public sealed record GetRolesQuery() : IQuery<IReadOnlyList<RoleResponse>>;

