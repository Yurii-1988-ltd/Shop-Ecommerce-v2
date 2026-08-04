namespace Ecommerce.Catalog.Modules.Application.Features.Products.Responses;

public record ProductResponse(
    Guid Id,
    string Name,
    string Description,
    string Sku,
    decimal Price,
    string Currency);
