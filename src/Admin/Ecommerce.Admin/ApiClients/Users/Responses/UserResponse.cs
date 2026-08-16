namespace Ecommerce.Admin.ApiClients.Users.Responses;

public record UserResponse(Guid Id,
string Email,
string FirstName,
string LastName, DateTime CreatedAtUtc);
