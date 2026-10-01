using Ecommerce.Catalog.Modules.Application.Abstractions.Data;
using Ecommerce.Catalog.Modules.Application.Mapping;
using Ecommerce.Localization.Abstractions.Abstractions;
using MongoDB.Driver;

namespace Ecommerce.Catalog.Modules.Application.Features.Products.GetProduct;

internal sealed class GetProductQueryHandler(
    ICatalogDatabase context,
    ILocalizationService localization,
    ICurrentCulture currentCulture)
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
        {
            var message = await localization.GetAsync(
                "Product.NotFound",
                "Catalog",
                currentCulture.Culture,
                cancellationToken);

            return ProductErrors.NotFound(message);
        }

        return product.ToResponse();
    }
}