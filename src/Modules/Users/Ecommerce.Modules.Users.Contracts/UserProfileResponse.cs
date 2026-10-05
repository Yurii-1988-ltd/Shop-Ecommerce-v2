

namespace Ecommerce.Modules.Users.Application.Responses;

public sealed record UserProfileResponse(Guid Id,
     string FirstName,
     string LastName,
     string Email,
    string PhoneNumber);
