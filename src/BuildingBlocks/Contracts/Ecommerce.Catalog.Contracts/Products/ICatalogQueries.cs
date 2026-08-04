

using Ecommerce.Domain.Domain;

namespace Ecommerce.Catalog.Contracts.Products
{
    public interface ICatalogQueries
    {
        Task<Result<ProductDto?>> GetProductAsync(
        Guid productId,
        CancellationToken cancellationToken);
    }
}
