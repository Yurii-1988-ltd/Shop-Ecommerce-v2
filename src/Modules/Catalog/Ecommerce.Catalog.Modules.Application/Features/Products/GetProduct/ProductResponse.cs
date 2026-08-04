using Ecommerce.Catalog.Modules.Application.Features.Products.Responses;

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
    IReadOnlyCollection<ProductImageResponse> Images,
    DateTime CreatedAtUtc,
    DateTime? UpdatedAtUtc);