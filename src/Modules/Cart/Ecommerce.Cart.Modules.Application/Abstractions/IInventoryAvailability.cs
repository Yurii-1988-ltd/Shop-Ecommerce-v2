

namespace Ecommerce.Cart.Modules.Application.Abstractions;

public interface IInventoryAvailability
{
    Task<int?> GetAvailableQuantityAsync(
        Guid productId,
        CancellationToken cancellationToken = default);
}
