using Ecommerce.Domain.Domain;
using Ecommerce.Inventory.Modules.Domain.Entities;

public interface IInventoryRepository
{
    Task<InventoryItem?> GetAsync(
        Guid inventoryItemId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        InventoryItem inventoryItem,
        CancellationToken cancellationToken = default);
    Task<InventoryItem?>GetByProductByIdAsync(
        Guid productId,
        CancellationToken cancellationToken = default);

}