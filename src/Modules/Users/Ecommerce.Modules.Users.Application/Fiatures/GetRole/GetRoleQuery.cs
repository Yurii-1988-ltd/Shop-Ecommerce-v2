
using Ecommerce.Modules.Users.Contracts.Dto;

namespace Ecommerce.Modules.Users.Application.Fiatures.GetRole;

public sealed record GetRoleQuery(Guid id) : IQuery<RoleResponse>;

