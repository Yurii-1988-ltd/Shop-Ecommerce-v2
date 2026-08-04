

namespace Ecommerce.Order.Modules.Application.Features.Responses;

public sealed record AddressResponse(

    string FirstName,
    string LastName,
    string Country,
    string City,
    string Street,
    string ZipCode

    );
