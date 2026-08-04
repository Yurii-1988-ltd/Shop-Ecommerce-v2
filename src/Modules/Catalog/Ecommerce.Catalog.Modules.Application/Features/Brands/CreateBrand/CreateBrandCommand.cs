

using Ecommerce.Application.CQRS;

namespace Ecommerce.Catalog.Modules.Application.Features.Brands.CreateBrand;

public sealed record CreateBrandCommand(string Name, string Description) : IQuery<Guid>;

