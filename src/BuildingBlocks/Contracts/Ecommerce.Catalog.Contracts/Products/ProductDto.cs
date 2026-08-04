namespace Ecommerce.Catalog.Contracts;

public sealed record ProductDto(
    Guid Id,
    string Name,
    string Sku,
    decimal Amount,
    string Currency);