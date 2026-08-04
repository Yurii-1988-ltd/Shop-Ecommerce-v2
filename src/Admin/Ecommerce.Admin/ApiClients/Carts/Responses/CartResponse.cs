public sealed record CartResponse(
    Guid Id,
    Guid CustomerId,
    IReadOnlyList<CartItemResponse> Items,
    int TotalItems,
    decimal TotalAmount,
    string Currency);