
using Ecommerce.Application.CQRS;
using Ecommerce.Catalog.Modules.Application.Abstractions.Data;
using Ecommerce.Catalog.Modules.Application.Mapping;
using Ecommerce.Catalog.Modules.Domain.Errors;
using Ecommerce.Domain.Domain;
using MongoDB.Driver;

namespace Ecommerce.Catalog.Modules.Application.Features.Categories.GetCategory;

internal sealed class GetCategoryQueryHandler(ICatalogDatabase context) : IQueryHandler<GetCategoryQuery, CategoryResponse>
{
    public async Task<Result<CategoryResponse>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await context.Categories
           .Find(x => x.Id == request.Id)
           .FirstOrDefaultAsync(cancellationToken);

        if (category is null)
            return CategoryErrors.NotFound(request.Id);
       

        return category.ToResponse();

    }
}
