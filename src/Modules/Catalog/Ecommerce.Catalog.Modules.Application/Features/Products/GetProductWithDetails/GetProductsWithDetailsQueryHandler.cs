
using Ecommerce.Application.CQRS;
using Ecommerce.Catalog.Modules.Application.Abstractions.Data;
using Ecommerce.Catalog.Modules.Application.Mapping;
using Ecommerce.Catalog.Modules.Domain.Entities;
using Ecommerce.Catalog.Modules.Domain.Errors;
using Ecommerce.Domain.Domain;
using MongoDB.Driver;

namespace Ecommerce.Catalog.Modules.Application.Features.Products.GetProductWithDetails;

internal sealed class GetProductsWithDetailsQueryHandler(ICatalogDatabase context) 
                        : IQueryHandler<GetProductsWithDetailsQuery, ProductDetailsResponse>
{
    public async Task<Result<ProductDetailsResponse>> Handle(
     GetProductsWithDetailsQuery request,
     CancellationToken cancellationToken)
    {
        var product = await context.Products
            .Find(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (product is null)
            return ProductErrors.NotFound(request.Id);

        Category? category = null;

        if (product.CategoryId.HasValue)
        {
            category = await context.Categories
                .Find(x => x.Id == product.CategoryId.Value)
                .FirstOrDefaultAsync(cancellationToken);
        }

        Brand? brand = null;

        if (product.BrandId.HasValue)
        {
            brand = await context.Brands
                .Find(x => x.Id == product.BrandId.Value)
                .FirstOrDefaultAsync(cancellationToken);
        }

        return product.ToDetailsResponse(category, brand);
    }
}
