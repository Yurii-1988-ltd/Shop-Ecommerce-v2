

using Ecommerce.Catalog.Contracts;
using Ecommerce.Order.Modules.Application.Contracts;

namespace Ecommerce.Order.Modules.Application.Services;

public interface IProductService
{
    Task<Result<ProductInfo>> GetProductAsync(
       Guid productId,
       CancellationToken cancellationToken);
}
