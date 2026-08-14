namespace Ecommerce.Modules.Users.Application.Fiatures.UpdateRole
{
    public sealed record UpdateRoleCommand(Guid Id, string Name) : ICommand;
   
}
