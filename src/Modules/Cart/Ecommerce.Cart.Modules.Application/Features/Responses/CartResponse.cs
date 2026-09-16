namespace Ecommerce.Cart.Modules.Application.Features.Responses;

public sealed record CartResponse(
    Guid Id,
    Guid? CustomerId,
    Guid? GuestId,
    IReadOnlyList<CartItemResponse> Items,
    int TotalItems,
    decimal Subtotal,
    decimal Discount,
    decimal TotalAmount,
    string Currency,
    string Code);
