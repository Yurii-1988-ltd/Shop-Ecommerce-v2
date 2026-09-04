namespace Ecommerce.Storefront.ApiClients.Catalogs.Models;

public record ProductDetailsResponse(

 Guid Id,
string Name,
string Description,
decimal Price,
string Currency,
decimal? SalePrice,
bool IsAvailable,
IReadOnlyCollection<ProductImageResponse> Images);

