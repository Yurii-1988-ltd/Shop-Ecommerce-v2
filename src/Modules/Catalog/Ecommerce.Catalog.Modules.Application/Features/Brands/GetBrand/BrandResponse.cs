

namespace Ecommerce.Catalog.Modules.Application.Features.Brands.GetBrand;

public sealed record BrandResponse(Guid Id,
string Name,
string? Description,
bool IsActive,
DateTime CreatedAtUtc,
DateTime? UpdatedAtUtc);

