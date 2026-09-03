namespace Ecommerce.Storefront.Models;

public sealed record ProductListItemResponse(
    Guid Id,
    string Name,
    decimal Price,
    string Currency,
    decimal? SalePrice,
    string? ImageUrl,
    bool IsAvailable);
