

using Ecommerce.Application.CQRS;
using Ecommerce.Application.Pagination;
using Ecommerce.Catalog.Modules.Application.Abstractions.Data;
using Ecommerce.Catalog.Modules.Application.Mapping;
using Ecommerce.Catalog.Modules.Domain.Entities;
using Ecommerce.Domain.Domain;
using MongoDB.Driver;

namespace Ecommerce.Catalog.Modules.Application.Features.Brands.GetBrands;

internal sealed class GetBrandsQueryHandler(ICatalogDatabase context) : IQueryHandler<GetBrandsQuery, PagedResult<BrandListItemResponse>>
{
    public async Task<Result<PagedResult<BrandListItemResponse>>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<Brand>.Filter.Empty;

        var totalCount = (int)await context.Brands
            .CountDocumentsAsync(filter, cancellationToken: cancellationToken);

        var brands = await context.Brands
            .Find(filter)
            .Skip((request.Page - 1) * request.PageSize)
            .Limit(request.PageSize)
            .ToListAsync(cancellationToken);

        var items = brands
            .Select(x => x.ToBrandListItemResponse())
            .ToList();
        return new PagedResult<BrandListItemResponse>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };
    }
}

