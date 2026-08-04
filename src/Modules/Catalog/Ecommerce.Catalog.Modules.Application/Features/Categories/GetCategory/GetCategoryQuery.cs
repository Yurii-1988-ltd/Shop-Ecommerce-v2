
using Ecommerce.Application.CQRS;

namespace Ecommerce.Catalog.Modules.Application.Features.Categories.GetCategory;

public sealed record GetCategoryQuery(Guid Id) : IQuery<CategoryResponse>;

