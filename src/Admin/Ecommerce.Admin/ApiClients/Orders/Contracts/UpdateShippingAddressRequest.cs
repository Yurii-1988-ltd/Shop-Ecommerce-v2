namespace Ecommerce.Admin.ApiClients.Orders.Contracts;

public sealed record UpdateShippingAddressRequest(
    string FirstName,
    string LastName,
    string Country,
    string City,
    string Street,
    string ZipCode);
