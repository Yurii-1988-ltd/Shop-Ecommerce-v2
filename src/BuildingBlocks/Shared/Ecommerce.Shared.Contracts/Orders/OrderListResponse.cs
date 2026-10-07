public sealed record OrderListResponse(
    Guid Id,
    string OrderNumber,
    Guid? CustomerId,
    string? CustomerName,
    string Status,
    int TotalQuantity,
    decimal TotalAmount,
    string Currency,
    DateTime CreatedAtUtc);