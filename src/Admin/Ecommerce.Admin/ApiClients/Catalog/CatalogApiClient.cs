

using Ecommerce.Admin.ApiClients.Catalog.Contracts;
using Ecommerce.Admin.ApiClients.Catalog.Models;
using Ecommerce.Admin.Contracts;

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

        await EnsureSuccessAsync(response, cancellationToken);

        return await response.Content.ReadFromJsonAsync<Guid>(cancellationToken);

    }

    public async Task<PagedResult<ProductListItemResponse>?> GetProductsAsync(
      int page,
      int pageSize,
      string? search = null,
      CancellationToken cancellationToken = default)
    {
        var query = $"?page={page}&pageSize={pageSize}";

        if (!string.IsNullOrWhiteSpace(search))
        {
            query += $"&search={Uri.EscapeDataString(search.Trim())}";
        }

        return await httpClient.GetFromJsonAsync<
            PagedResult<ProductListItemResponse>>(
                $"{ProductsUrl}{query}",
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
        await EnsureSuccessAsync(response, cancellationToken);
    }

 
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var response = await httpClient.DeleteAsync($"{ProductsUrl}/{id}", cancellationToken);
        await EnsureSuccessAsync(response, cancellationToken);


    }

    private async Task EnsureSuccessAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode)
            return;
        var error = await response.Content.ReadFromJsonAsync<ApiError>(cancellationToken);
        if (error != null)
            throw new ApiException(error);
        throw new HttpRequestException($"Request failed with status code {response.StatusCode}");

    }
}
