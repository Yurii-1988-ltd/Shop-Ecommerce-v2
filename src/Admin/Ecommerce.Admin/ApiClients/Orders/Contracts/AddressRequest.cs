namespace Ecommerce.Admin.ApiClients.Orders.Contracts;

public sealed record AddressRequest(
string FirstName,
string LastName,
string Country,
string City,
string Street,
string ZipCode);
