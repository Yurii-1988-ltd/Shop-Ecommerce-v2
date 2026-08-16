using Ecommerce.Catalog.Modules.Application.Abstractions.Data;
using Ecommerce.Catalog.Modules.Application.Mapping;
using MongoDB.Driver;

namespace Ecommerce.Catalog.Modules.Application.Features.Brands.GetBrand;

internal sealed class GetBrandsQueryHandler(ICatalogDatabase context) : IQueryHandler<GetBrandQuery, BrandResponse>
{
    public async Task<Result<BrandResponse>> Handle(GetBrandQuery request, CancellationToken cancellationToken)
    {
        var brand = await context.Brands
         .Find(x => x.Id == request.Id)
         .FirstOrDefaultAsync(cancellationToken);

        if (brand is null)
            return CategoryErrors.NotFound(request.Id);


        return brand.ToResponse();
    }
}
