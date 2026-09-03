using Ecommerce.Application.Pagination;
using Ecommerce.Storefront.Models;

namespace Ecommerce.Storefront.ApiClients;

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
