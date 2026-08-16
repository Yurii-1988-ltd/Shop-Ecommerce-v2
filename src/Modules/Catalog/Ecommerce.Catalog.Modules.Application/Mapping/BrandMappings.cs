

using Ecommerce.Catalog.Modules.Application.Features.Brands.GetBrand;
using Ecommerce.Catalog.Modules.Application.Features.Brands.GetBrands;
using Ecommerce.Catalog.Modules.Application.Features.Categories.GetCategory;
using Ecommerce.Catalog.Modules.Domain.Entities;

namespace Ecommerce.Catalog.Modules.Application.Mapping;

internal static class BrandMappings
{
    public static BrandListItemResponse ToBrandListItemResponse(this Brand brand)
    {
        return new(
            brand.Id,
            brand.Name,
            brand.Description,
            brand.IsActive);
    }
    public static BrandResponse ToResponse(this Brand brand)
    {
        return new BrandResponse(
            brand.Id,
            brand.Name,
            brand.Description,
            brand.IsActive,
            brand.CreatedAtUtc,
            brand.UpdatedAtUtc
            );
    }
}
