
namespace Ecommerce.Modules.Users.Application.Fiatures.UpdateUser;

public sealed record UpdateUserCommand(Guid Id, string FirstName, string LastName, string Email, string PhoneNumber) : ICommand;

