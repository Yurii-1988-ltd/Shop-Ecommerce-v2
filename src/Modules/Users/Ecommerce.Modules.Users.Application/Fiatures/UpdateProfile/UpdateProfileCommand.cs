
namespace Ecommerce.Modules.Users.Application.Fiatures.UpdateProfile;

public sealed record UpdateProfileCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber) : ICommand;

