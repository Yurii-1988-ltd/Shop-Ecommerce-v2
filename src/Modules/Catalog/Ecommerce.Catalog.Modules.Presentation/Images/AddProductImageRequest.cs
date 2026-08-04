namespace Ecommerce.Catalog.Modules.Presentation.Images;

public sealed record AddProductImageRequest(
    string StorageKey,
    string? AltText);