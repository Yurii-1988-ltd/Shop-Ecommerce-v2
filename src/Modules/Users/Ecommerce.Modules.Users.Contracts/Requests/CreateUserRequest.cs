

namespace Ecommerce.Modules.Users.Contracts.Requests;

public sealed record CreateUserRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName);

