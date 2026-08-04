

namespace Ecommerce.Catalog.Modules.Application.Features.Categories.GetCategory;

public record CategoryResponse(   
  Guid Id ,
string Name ,
 string? Description,
 bool IsActive,
 DateTime CreatedAtUtc ,
 DateTime? UpdatedAtUtc );

