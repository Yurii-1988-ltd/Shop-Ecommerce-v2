using Ecommerce.Admin.ApiClients.Catalog.Contracts;
using Ecommerce.Admin.ApiClients.Catalog.Models;
using Ecommerce.Admin.Contracts;
using Ecommerce.Application.Pagination;

namespace Ecommerce.Admin.ApiClients.Catalog;

public interface ICatalogApiClient
{
    Task<Guid> CreateAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken = default);

    Task<PagedResult<ProductListItemResponse>?> GetProductsAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);

    Task<ProductResponse?> GetAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Guid id,
        UpdateProductRequest request,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}