using Ardalis.Specification.EntityFrameworkCore; // Или Ardalis.Specification.IQueryable
using Ecommerce.Application.CQRS;
using Ecommerce.Application.Pagination;
using Ecommerce.Catalog.Modules.Application.Abstractions.Data;
using Ecommerce.Catalog.Modules.Application.Mapping;
using Ecommerce.Catalog.Modules.Domain.Entities;
using Ecommerce.Catalog.Modules.Domain.Specifications;
using Ecommerce.Domain.Domain;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Ecommerce.Catalog.Modules.Application.Features.Products.GetProducts;

internal sealed class GetProductsQueryHandler(
    ICatalogDatabase context)
    : IQueryHandler<GetProductsQuery, PagedResult<ProductListItemResponse>>
{
    public async Task<Result<PagedResult<ProductListItemResponse>>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        // 1. Создаем экземпляр спецификации из параметров запроса
        var spec = new ProductFilterAndSortSpec(
            categoryId: request.CategoryId,
            brandId: request.BrandId,
            minPrice: request.MinPrice,
            maxPrice: request.MaxPrice,
            inStockOnly: request.InStockOnly,
            sortOption: request.SortOption,
            page: request.Page,
            pageSize: request.PageSize);

        // 2. Получаем IQueryable от MongoDB
        var query = context.Products.AsQueryable();

        // Если есть поисковый запрос по строке, добавляем его в LINQ-цепочку
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim().ToLower();
            query = query.Where(x =>
                x.Name.ToLower().Contains(search) ||
                x.Sku.ToLower().Contains(search) ||
                x.ProductNumber.ToLower().Contains(search));
        }

        // 3. Подсчет общего количества ДО применения Skip/Take (пагинации)
        // Применяем спецификацию БЕЗ учета Skip/Take для Count
        var countSpec = new ProductFilterAndSortSpec(
            categoryId: request.CategoryId,
            brandId: request.BrandId,
            minPrice: request.MinPrice,
            maxPrice: request.MaxPrice,
            inStockOnly: request.InStockOnly,
            sortOption: request.SortOption,
            page: 0,       // Сбрасываем пагинацию
            pageSize: 0);  // чтобы посчитать общее кол-во

        var totalCountQuery = SpecificationEvaluator.Default.GetQuery(query, countSpec);
        var totalCount = await totalCountQuery.CountAsync(cancellationToken);

        // 4. Применяем полную спецификацию (с фильтрами, сортировкой и Skip/Take)
        var filteredQuery = SpecificationEvaluator.Default.GetQuery(query, spec);
        var products = await filteredQuery.ToListAsync(cancellationToken);

        // 5. Маппинг и возврат результата
        var items = products
            .Select(x => x.ToListItemResponse())
            .ToList();

        return Result<PagedResult<ProductListItemResponse>>.Success(
            new PagedResult<ProductListItemResponse>
            {
                Items = items,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount
            });
    }
}