using Ecommerce.Admin.ApiClients.Catalog.Contracts;
using Ecommerce.Admin.ApiClients.Catalog.Models;
using Ecommerce.Admin.ApiClients.Categories;
using Ecommerce.Admin.Contracts;
using Ecommerce.Application.Pagination;

namespace Ecommerce.Admin.ApiClients.Catalog;

internal sealed class CatalogApiClient(HttpClient httpClient) : ICatalogApiClient
{
    private const string ProductsUrl  ="/products"; 
    public async Task<Guid> CreateAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync(
            $"{ProductsUrl}",
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Guid>(cancellationToken);

    }

    public async Task<PagedResult<ProductListItemResponse>?> GetProductsAsync(
     int page,
     int pageSize,
     CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<PagedResult<ProductListItemResponse>>(
            $"{ProductsUrl}?page={page}&pageSize={pageSize}",
            cancellationToken);
    }

    public async Task<ProductResponse?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<ProductResponse>($"{ProductsUrl}/{id}", cancellationToken);
    }

    public async Task UpdateAsync(Guid id, UpdateProductRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"{ProductsUrl}/{id}",
                                                            request,
                                                            cancellationToken);
        response.EnsureSuccessStatusCode();
        
    }

 
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var response = await httpClient.DeleteAsync($"{ProductsUrl}/{id}", cancellationToken);

        response.EnsureSuccessStatusCode();
    }
}
