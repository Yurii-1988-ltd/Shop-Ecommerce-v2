

namespace Ecommerce.Modules.Users.Contracts.Requests;

public sealed record CreateUserRequest(
    string Email,
    string PasswordHash,
    string FirstName,
    string LastName);

