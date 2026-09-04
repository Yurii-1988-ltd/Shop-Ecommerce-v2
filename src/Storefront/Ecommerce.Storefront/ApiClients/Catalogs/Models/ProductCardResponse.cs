namespace Ecommerce.Storefront.ApiClients.Catalogs.Models;

public sealed record ProductCardResponse(
    Guid Id,
    string Name,
    decimal Price,
    string Currency,
    decimal? SalePrice,
    string? ImageUrl,
    bool IsAvailable);
