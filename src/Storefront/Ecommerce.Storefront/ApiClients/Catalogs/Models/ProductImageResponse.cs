namespace Ecommerce.Storefront.ApiClients.Catalogs.Models;

    public record ProductImageResponse(Guid Id,
    string Url,
    string ThumbnailUrl,
    string AltText,
    bool IsPrimary,
    int DisplayOrder);
   

