namespace Ecommerce.Storefront.ApiClients.Catalogs.Models;

public sealed record ProductDetailsResponse(
    Guid Id,
    string ProductNumber,
    string Name,
    string Description,
    string Sku,
    decimal Price,
    string Currency,
    decimal? SalePrice,
    int StockQuantity,
    bool IsActive,
    Guid? CategoryId,
    Guid? BrandId,
    IReadOnlyCollection<ProductImageResponse> Images,
    DateTime CreatedAtUtc,
    DateTime? UpdatedAtUtc);