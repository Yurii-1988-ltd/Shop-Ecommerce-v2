public sealed record CartResponse(
    Guid Id,
    Guid CustomerId,
    IReadOnlyList<CartItemResponse> Items,
    int TotalItems,
    decimal SubTotal,
    decimal Discount,
    decimal TotalAmount,
    string Currency,
    string? CouponCode
    );