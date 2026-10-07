using Ecommerce.Application.Pagination;
using Ecommerce.Domain.Enums;
using Ecommerce.Storefront.ApiClients.Catalogs.Models;
using Microsoft.AspNetCore.WebUtilities;

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

        public async Task<PagedResult<ProductListItemResponse>?> GetProductsAsync(
         int page = 1,
         int pageSize = 20,
         string? search = null,
         Guid? categoryId = null,
         Guid? brandId = null,
         decimal? minPrice = null,
         decimal? maxPrice = null,
         bool inStockOnly = false,
         ProductSortOptions sortOption = ProductSortOptions.Relevance,
         CancellationToken cancellationToken = default)
        {
            var queryParams = new Dictionary<string, string?>
            {
                ["page"] = page.ToString(),
                ["pageSize"] = pageSize.ToString(),
                ["sortOption"] = sortOption.ToString()
            };

            if (!string.IsNullOrWhiteSpace(search))
                queryParams["search"] = search;

            if (categoryId.HasValue)
                queryParams["categoryId"] = categoryId.Value.ToString();

            if (brandId.HasValue)
                queryParams["brandId"] = brandId.Value.ToString();

            if (minPrice.HasValue)
                queryParams["minPrice"] = minPrice.Value.ToString();

            if (maxPrice.HasValue)
                queryParams["maxPrice"] = maxPrice.Value.ToString();

            if (inStockOnly)
                queryParams["inStockOnly"] = "true";

            var uri = QueryHelpers.AddQueryString("/products", queryParams);

            return await httpClient.GetFromJsonAsync<PagedResult<ProductListItemResponse>>(uri, cancellationToken);
        }
    }
    
}
