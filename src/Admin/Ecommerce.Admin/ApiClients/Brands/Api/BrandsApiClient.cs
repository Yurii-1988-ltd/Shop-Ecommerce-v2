using Ecommerce.Admin.ApiClients.Brands.Contracts;
using Ecommerce.Admin.ApiClients.Brands.Models;

using Ecommerce.Application.Pagination;

namespace Ecommerce.Admin.ApiClients.Brands.Api;

internal sealed class BrandsApiClient(HttpClient httpClient) : IBrandApiClient
{
    private const string BrandsUrl  ="/brands"; 
    public async Task<Guid> CreateAsync(
        CreateBrandRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync(
            $"{BrandsUrl}",
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Guid>(cancellationToken);

    }

    public async Task<PagedResult<BrandListItemResponse>?> GetAllAsync(
     int page,
     int pageSize,
     CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<PagedResult<BrandListItemResponse>>(
            $"{BrandsUrl}?page={page}&pageSize={pageSize}",
            cancellationToken);
    }

    public async Task<BrandResponse?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<BrandResponse>($"{BrandsUrl}/{id}", cancellationToken);
    }

    public async Task UpdateAsync(Guid id, UpdateBrandRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"{BrandsUrl}/{id}",
                                                            request,
                                                            cancellationToken);
        response.EnsureSuccessStatusCode();
        
    }

 
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var response = await httpClient.DeleteAsync($"{BrandsUrl}/{id}", cancellationToken);

        response.EnsureSuccessStatusCode();
    }
}
