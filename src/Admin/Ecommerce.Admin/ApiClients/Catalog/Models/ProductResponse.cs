using Ecommerce.Admin.ApiClients.Images.Models;

namespace Ecommerce.Admin.ApiClients.Catalog.Models;

public sealed record ProductResponse(
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
    DateTime CreatedAtUtc,
    DateTime? UpdatedAtUtc,
IReadOnlyCollection<ProductImageResponse> Images );