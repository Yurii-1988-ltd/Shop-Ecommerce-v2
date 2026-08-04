using Ecommerce.Catalog.Modules.Application.Abstractions.Data;
using Ecommerce.Catalog.Modules.Application.Mapping;
using MongoDB.Driver;

namespace Ecommerce.Catalog.Modules.Application.Features.Products.GetProduct;

internal sealed class GetProductQueryHandler(
    ICatalogDatabase context)
    : IQueryHandler<GetProductQuery, ProductResponse>
{
    public async Task<Result<ProductResponse>> Handle(
        GetProductQuery request,
        CancellationToken cancellationToken)
    {
        var product = await context.Products
            .Find(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (product is null)
       
            return ProductErrors.NotFound(request.Id);

        return product.ToResponse();
    }
}