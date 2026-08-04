public sealed record CartListResponse(
    Guid Id,
    Guid CustomerId,
    int TotalItems,
    decimal TotalAmount,
    string Currency);