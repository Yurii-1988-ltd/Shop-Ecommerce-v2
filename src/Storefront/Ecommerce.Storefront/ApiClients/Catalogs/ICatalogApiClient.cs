using Ecommerce.Application.Pagination;
using Ecommerce.Domain.Enums;
using Ecommerce.Storefront.ApiClients.Catalogs.Models;

namespace Ecommerce.Storefront.ApiClients.Catalogs;

public interface ICatalogApiClient
{
    Task<PagedResult<ProductListItemResponse>?> GetProductsAsync(
        int page = 1,
        int pageSize = 20,
        string? search = null,
        Guid? categoryId = null,
        Guid? brandId = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        bool inStockOnly = false,
        ProductSortOptions sortOption = ProductSortOptions.Relevance,
        CancellationToken cancellationToken = default);

    Task<ProductDetailsResponse?> GetAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
