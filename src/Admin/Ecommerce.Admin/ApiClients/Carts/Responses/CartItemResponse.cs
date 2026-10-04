public sealed record CartItemResponse(
    Guid ProductId,
    string Name,
    string Sku,
    decimal Price,
    string Currency,
    int Quantity,
    decimal TotalPrice);