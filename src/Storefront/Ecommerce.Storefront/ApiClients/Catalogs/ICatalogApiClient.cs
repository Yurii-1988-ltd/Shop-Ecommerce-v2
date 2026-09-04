using Ecommerce.Application.Pagination;
using Ecommerce.Storefront.ApiClients.Catalogs.Models;

namespace Ecommerce.Storefront.ApiClients.Catalogs;

public interface ICatalogApiClient
{
    Task<PagedResult<ProductListItemResponse>?> GetProductsAsync(
        int page,
        int pageSize,
        string? search = null,
        CancellationToken cancellationToken = default);

    Task<ProductDetailsResponse?> GetAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
