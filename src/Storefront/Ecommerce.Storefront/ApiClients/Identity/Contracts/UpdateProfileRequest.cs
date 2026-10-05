namespace Ecommerce.Storefront.ApiClients.Identity.Contracts;

public sealed record UpdateProfileRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber);

