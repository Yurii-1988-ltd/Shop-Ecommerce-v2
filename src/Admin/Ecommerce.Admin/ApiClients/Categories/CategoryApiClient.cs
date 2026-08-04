using Ecommerce.Admin.ApiClients.Catalog.Models;
using Ecommerce.Admin.ApiClients.Categories.Contracts;
using Ecommerce.Admin.ApiClients.Categories.Models;
using Ecommerce.Application.Pagination;

namespace Ecommerce.Admin.ApiClients.Categories;

internal sealed class CategoryApiClient(HttpClient httpClient) : ICategoryApiClient
{
    private const string CategoriesUrl  ="/categories"; 
    public async Task<Guid> CreateAsync(
        CreateCategoryRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync(
            $"{CategoriesUrl}",
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Guid>(cancellationToken);

    }

    public async Task<PagedResult<CategoryListItemResponse>?> GetAllAsync(
     int page,
     int pageSize,
     CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<PagedResult<CategoryListItemResponse>>(
            $"{CategoriesUrl}?page={page}&pageSize={pageSize}",
            cancellationToken);
    }

    public async Task<CategoryResponse?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<CategoryResponse>($"{CategoriesUrl}/{id}", cancellationToken);
    }

    public async Task UpdateAsync(Guid id, UpdateCategoryRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"{CategoriesUrl}/{id}",
                                                            request,
                                                            cancellationToken);
        response.EnsureSuccessStatusCode();
        
    }

 
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var response = await httpClient.DeleteAsync($"{CategoriesUrl}/{id}", cancellationToken);

        response.EnsureSuccessStatusCode();
    }
}
