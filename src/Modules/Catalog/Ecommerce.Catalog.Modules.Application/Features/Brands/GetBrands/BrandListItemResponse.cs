namespace Ecommerce.Catalog.Modules.Application.Features.Brands.GetBrands
{
    public sealed record BrandListItemResponse(  
      Guid Id,
     string Name,
     string? Description,
     bool IsActive );
    
}