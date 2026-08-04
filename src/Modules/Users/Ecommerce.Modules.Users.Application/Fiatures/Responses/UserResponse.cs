

namespace Ecommerce.Modules.Users.Application.Fiatures.Responses;

public record UserResponse(Guid Id,
string Email,
string FirstName,
string LastName, DateTime CreatedAtUtc);

