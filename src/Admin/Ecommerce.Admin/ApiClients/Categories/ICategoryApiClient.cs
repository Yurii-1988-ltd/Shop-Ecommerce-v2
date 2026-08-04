using Ecommerce.Admin.ApiClients.Catalog.Contracts;
using Ecommerce.Admin.ApiClients.Catalog.Models;
using Ecommerce.Admin.ApiClients.Categories.Contracts;
using Ecommerce.Admin.ApiClients.Categories.Models;
using Ecommerce.Admin.Contracts;
using Ecommerce.Application.Pagination;

namespace Ecommerce.Admin.ApiClients.Categories;

public interface ICategoryApiClient
{
    Task<Guid> CreateAsync(
        CreateCategoryRequest request,
        CancellationToken cancellationToken = default);

    Task<PagedResult<CategoryListItemResponse>?> GetAllAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);

    Task<CategoryResponse?> GetAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Guid id,
        UpdateCategoryRequest request,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}