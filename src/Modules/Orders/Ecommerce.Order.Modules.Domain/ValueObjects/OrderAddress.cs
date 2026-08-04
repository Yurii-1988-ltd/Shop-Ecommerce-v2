

namespace Ecommerce.Order.Modules.Domain.ValueObjects;

public sealed record OrderAddress(
    string FirstName,
    string LastName,
    string Country,
    string City,
    string Street,
    string ZipCode

);

