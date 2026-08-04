

using DnsClient;
using Ecommerce.Application.CQRS;
using Ecommerce.Domain.Domain;

namespace Ecommerce.Catalog.Modules.Application.Features.Brands.GetBrand;

internal sealed class GetBrandsQueryHandler : IQueryHandler<GetBrandQuery, BrandResponse>
{
    public async Task<Result<BrandResponse>> Handle(GetBrandQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
