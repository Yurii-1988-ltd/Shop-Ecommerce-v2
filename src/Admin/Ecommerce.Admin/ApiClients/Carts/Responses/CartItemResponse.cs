public sealed record CartItemResponse(
    Guid ProductId,
    string Name,
    decimal Price,
    string Currency,
    int Quantity,
    decimal TotalPrice);