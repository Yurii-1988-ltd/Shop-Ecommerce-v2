

namespace Ecommerce.Cart.Modules.Domain.Repositories;

public interface IInventoryAvailability
{
    Task<int?> GetAvailableQuantityAsync(
        Guid productId,
        CancellationToken cancellationToken = default);
}
