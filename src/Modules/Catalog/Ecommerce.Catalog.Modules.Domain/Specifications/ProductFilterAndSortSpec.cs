using Ardalis.Specification;
using Ecommerce.Catalog.Modules.Domain.Entities;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Catalog.Modules.Domain.Specifications;

public sealed class ProductFilterAndSortSpec : Specification<Product>
{
    public ProductFilterAndSortSpec(
        Guid? categoryId = null,
        Guid? brandId = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        bool inStockOnly = false,
        ProductSortOptions? sortOption = ProductSortOptions.Relevance,
        int page = 1,
        int pageSize = 12)
    {
        // 1. Фильтрация
        Query.Where(p => p.IsActive);

        if (categoryId.HasValue)
            Query.Where(p => p.CategoryId == categoryId.Value);

        if (brandId.HasValue)
            Query.Where(p => p.BrandId == brandId.Value);

        if (minPrice.HasValue)
            Query.Where(p => (p.SalePrice != null ? p.SalePrice.Amount : p.Price.Amount) >= minPrice.Value);

        if (maxPrice.HasValue)
            Query.Where(p => (p.SalePrice != null ? p.SalePrice.Amount : p.Price.Amount) <= maxPrice.Value);

        if (inStockOnly)
            Query.Where(p => p.StockQuantity > 0);

        // 2. Сортировка
        switch (sortOption)
        {
            case ProductSortOptions.PriceAsc:
                Query.OrderByDescending(p => p.StockQuantity > 0)
                     .ThenBy(p => p.SalePrice != null ? p.SalePrice.Amount : p.Price.Amount)
                     .ThenByDescending(p => p.CreatedAtUtc);
                break;

            case ProductSortOptions.PriceDesc:
                Query.OrderByDescending(p => p.StockQuantity > 0)
                     .ThenByDescending(p => p.SalePrice != null ? p.SalePrice.Amount : p.Price.Amount)
                     .ThenByDescending(p => p.CreatedAtUtc);
                break;

            case ProductSortOptions.Newest:
                Query.OrderByDescending(p => p.StockQuantity > 0)
                     .ThenByDescending(p => p.CreatedAtUtc);
                break;

            default: // Relevance / Default
                Query.OrderByDescending(p => p.StockQuantity > 0)
                     .ThenByDescending(p => p.CreatedAtUtc);
                break;
        }

        // 3. Подгрузка изображений (EF Core подгрузит всю коллекцию Images для сущности)
        Query.Include(p => p.Images);

        // 4. Пагинация
        if (page > 0 && pageSize > 0)
        {
            Query.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}