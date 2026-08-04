namespace Ecommerce.Admin.ApiClients.Catalog.Models;

public record ProductListItemResponse
(
    Guid Id,
    string Name,
    string Sku,
    string Description,
    decimal Price,
    string Currency,
    int StockQuantity,
    bool IsActive,
    string? ImageUrl


);