using Ecommerce.Application.Pagination;
using Ecommerce.Catalog.Modules.Application.Features.Products.GetProducts;
using Ecommerce.Domain.Enums;


public sealed record GetProductsQuery(
    int Page = 1,
    int PageSize = 12,
    string? Search = null,
    Guid? CategoryId = null,
    Guid? BrandId = null,
    decimal? MinPrice = null,
    decimal? MaxPrice = null,
    bool InStockOnly = false,
    ProductSortOptions SortOption = ProductSortOptions.Relevance)
    : PagedQuery<ProductListItemResponse>(Page, PageSize);