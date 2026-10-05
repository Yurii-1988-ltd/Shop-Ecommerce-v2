namespace Ecommerce.Storefront.ApiClients.Identity.Contracts;

public sealed record UserProfileResponse
(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber
);
