using Ecommerce.Application.Pagination;
using Ecommerce.Storefront.ApiClients.Catalogs.Models;

namespace Ecommerce.Storefront.ApiClients.Catalogs
{
    internal sealed class CatalogApiClient(HttpClient httpClient) : ICatalogApiClient
    {
        private const string ProductsUrl = "/products";
        public  async Task<ProductDetailsResponse?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var response = await httpClient.GetAsync(
                $"{ProductsUrl}/{id}",
                cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ProductDetailsResponse>(
                cancellationToken);

        }

        public  async Task<PagedResult<ProductListItemResponse>?> GetProductsAsync(int page, int pageSize, string? search = null, CancellationToken cancellationToken = default)
        {
            var url = $"{ProductsUrl}?page={page}&pageSize={pageSize}";

            if (!string.IsNullOrWhiteSpace(search))
            {
                url += $"&search={Uri.EscapeDataString(search)}";
            }

            return await httpClient.GetFromJsonAsync<
                PagedResult<ProductListItemResponse>>(
                    url,
                    cancellationToken);

        }
    }
}
