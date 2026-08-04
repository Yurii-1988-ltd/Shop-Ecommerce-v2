namespace Ecommerce.Catalog.Modules.Application.Features.Products.Responses;

public sealed record ProductImageResponse(
    Guid Id,
    string Url,
    string ThumbnailUrl,
    string AltText,
    bool IsPrimary,
    int DisplayOrder);