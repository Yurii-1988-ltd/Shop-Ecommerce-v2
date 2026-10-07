namespace Ecommerce.Storefront.ApiClients.Catalogs.Models;

public sealed record ProductListItemResponse(
    Guid Id,
    string ProductNumber,
    string Name,
    string Sku,
    decimal Price,
    string Currency,
    int StockQuantity,
    bool IsActive,
    string? ImageUrl);