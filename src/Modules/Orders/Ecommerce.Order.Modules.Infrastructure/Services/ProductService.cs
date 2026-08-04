using Ecommerce.Catalog.Contracts.Products;
using Ecommerce.Domain.Domain;
using Ecommerce.Domain.ValueObjects;
using Ecommerce.Order.Modules.Application.Contracts;

internal sealed class ProductService(
    ICatalogQueries catalogQueries)
    : IProductService
{
    public async Task<Result<ProductInfo>> GetProductAsync(Guid productId, CancellationToken cancellationToken)
    {
        var result = await catalogQueries.GetProductAsync(
              productId,
              cancellationToken);

        if (result.IsFailure)
            return Result.Failure<ProductInfo>(result.Error);

        var money = Money.Create(
            result.Value.Amount,
            result.Value.Currency);

        if (money.IsFailure)
            return Result.Failure<ProductInfo>(money.Error);

        return Result.Success(
            new ProductInfo(
                result.Value.Id,
                result.Value.Name,
                result.Value.Sku,
                money.Value));
    }
}
