public sealed record ProductDetailsResponse(
    Guid Id,
    string Name,
    string Description,
    string Sku,
    decimal Price,
    string Currency,
    int StockQuantity,
    bool IsActive,

    Guid? CategoryId,
    string? CategoryName,

    Guid? BrandId,
    string? BrandName);