
namespace Ecommerce.Modules.Users.Application.AssignRole;

public sealed record AssignRoleCommand(Guid UserId, Guid RoleId) : ICommand<Guid>;

