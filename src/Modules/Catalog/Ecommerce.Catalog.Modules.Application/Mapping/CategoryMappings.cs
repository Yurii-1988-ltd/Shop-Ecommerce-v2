using Ecommerce.Catalog.Modules.Application.Features.Categories.GetCategories;
using Ecommerce.Catalog.Modules.Application.Features.Categories.GetCategory;
using Ecommerce.Catalog.Modules.Domain.Entities;

namespace Ecommerce.Catalog.Modules.Application.Mapping;

    internal static class CategoryMappings
    {
    public static CategoryListItemResponse ToCategoryListItemResponse(this Category category)
    {
        return new(
            category.Id,
            category.Name,
            category.Description ,
            category.IsActive);
    }
    public static CategoryResponse ToResponse(this Category category)
    {
        return new CategoryResponse(
            category.Id,
            category.Name,
            category.Description,
            category.IsActive,
            category.CreatedAtUtc,
            category.UpdateAtUtc);
    }
    public static  ProductDetailsResponse ToDetailsResponse(this Product product,
                                                                Category? category,
                                                                Brand? brand)
    {
        return new ProductDetailsResponse(
    product.Id,
    product.Name,
    product.Description,
    product.Sku,
    product.Price.Amount,
    product.Price.Currency,
    product.StockQuantity,
    product.IsActive,
    product.CategoryId,
    category?.Name,
    product.BrandId,
    brand?.Name);

    }

}

