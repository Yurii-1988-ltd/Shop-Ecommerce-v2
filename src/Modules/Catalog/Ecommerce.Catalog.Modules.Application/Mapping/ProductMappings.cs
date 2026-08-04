using Ecommerce.Catalog.Modules.Application.Features.Products.GetProduct;
using Ecommerce.Catalog.Modules.Application.Features.Products.GetProducts;
using Ecommerce.Catalog.Modules.Application.Features.Products.Responses;
using Ecommerce.Catalog.Modules.Domain.Entities;

namespace Ecommerce.Catalog.Modules.Application.Mapping;

internal static class ProductMappings
{
    public static ProductListItemResponse ToListItemResponse(this Product product)
    {
        return new(
            product.Id,
            product.ProductNumber,
            product.Name,
        
            product.Sku,
            product.Price.Amount,
            product.Price.Currency,
            product.StockQuantity,
            product.IsActive,
            product.Images
                .OrderBy(i => i.DisplayOrder)
                .Select(i => $"/uploads/products/{i.StorageKey}")
                .FirstOrDefault());
    }

    public static ProductResponse ToResponse(this Product product)
    {
        return new ProductResponse(
            product.Id,
            product.ProductNumber,
            product.Name,
            product.Description,
            product.Sku,
            product.Price.Amount,
            product.Price.Currency,
            product.SalePrice?.Amount,
            product.StockQuantity,
            product.IsActive,
            product.CategoryId,
            product.BrandId,
            product.Images
                .OrderBy(x => x.DisplayOrder)
               .Select(x => new ProductImageResponse(
                    x.Id,
                    $"/uploads/products/{x.StorageKey}",
                    x.StorageKey,
                    x.AltText,
                    x.IsPrimary,
                    x.DisplayOrder))
                .ToList(),
            product.CreatedAtUtc,
            product.UpdatedAtUtc);
    }
}