

namespace Ecommerce.Modules.Users.Application.Fiatures.CreateRole;

public sealed record CreateRoleCommand(string Name) : ICommand<Guid>;

