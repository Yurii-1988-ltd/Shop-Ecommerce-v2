using Ecommerce.Application.Pagination;


namespace Ecommerce.Catalog.Modules.Application.Features.Categories.GetCategories;


public sealed record GetCategoriesQuery(
    int Page,
    int PageSize)
    : PagedQuery<CategoryListItemResponse>(Page, PageSize);

