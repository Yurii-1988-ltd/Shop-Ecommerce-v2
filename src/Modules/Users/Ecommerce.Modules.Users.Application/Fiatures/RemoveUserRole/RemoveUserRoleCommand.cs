

namespace Ecommerce.Modules.Users.Application.Fiatures.RemoveUserRole
{
    public sealed record RemoveUserRoleCommand(Guid userId, Guid roleId) : ICommand;
    
}
