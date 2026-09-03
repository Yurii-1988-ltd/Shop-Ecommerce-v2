using Ecommerce.Application.Pagination;

namespace Ecommerce.Catalog.Modules.Application.Features.Products.GetProducts;

public sealed record GetProductsQuery(
    int Page,
    int PageSize,
    string? Search = null)
    : PagedQuery<ProductListItemResponse>(Page, PageSize);

