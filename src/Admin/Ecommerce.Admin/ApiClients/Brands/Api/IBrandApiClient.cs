using Ecommerce.Admin.ApiClients.Brands.Contracts;
using Ecommerce.Admin.ApiClients.Brands.Models;

using Ecommerce.Application.Pagination;

namespace Ecommerce.Admin.ApiClients.Brands.Api;

public interface IBrandApiClient
{
    Task<Guid> CreateAsync(
        CreateBrandRequest request,
        CancellationToken cancellationToken = default);

    Task<PagedResult<BrandListItemResponse>?> GetAllAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);

    Task<BrandResponse?> GetAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Guid id,
        UpdateBrandRequest request,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}