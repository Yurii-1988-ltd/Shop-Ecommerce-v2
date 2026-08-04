using Ecommerce.Catalog.Contracts;
using Ecommerce.Catalog.Contracts.Products;
using Ecommerce.Catalog.Modules.Domain.Errors;
using Ecommerce.Domain.Domain;

namespace Ecommerce.Catalog.Modules.Infrastructure.Services
{
    internal sealed class CatalogQueries(
        IProductRepository repository)
        : ICatalogQueries
    {
        public async Task<Result<ProductDto>> GetProductAsync(
            Guid productId,
            CancellationToken cancellationToken)
        {
            var product = await repository.GetByIdAsync(
                productId,
                cancellationToken);

            if (product is null)
                return ProductErrors.NotFound(productId);

            return Result.Success(
                new ProductDto(
                    product.Id,
                    product.Name,
                    product.Sku,
                    product.Price.Amount,
                    product.Price.Currency));
        }
    }
}
