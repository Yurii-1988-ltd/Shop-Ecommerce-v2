
using Ecommerce.Application.CQRS;

namespace Ecommerce.Catalog.Modules.Application.Features.Brands.GetBrand;

public sealed record GetBrandQuery(Guid Id) : IQuery<BrandResponse>;

