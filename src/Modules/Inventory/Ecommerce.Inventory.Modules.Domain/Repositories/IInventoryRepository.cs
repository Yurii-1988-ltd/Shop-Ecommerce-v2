using Ecommerce.Inventory.Modules.Domain.Entities;

public interface IInventoryRepository
{
    Task<InventoryItem?> GetAsync(
        Guid inventoryItemId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        InventoryItem inventoryItem,
        CancellationToken cancellationToken = default);
}