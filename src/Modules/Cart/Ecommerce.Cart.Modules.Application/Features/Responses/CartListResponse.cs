namespace Ecommerce.Cart.Modules.Application.Features.Responses;

public sealed record CartListResponse(
    Guid Id,
    Guid CustomerId,
    int TotalItems,
    decimal TotalAmount,
    string Currency);