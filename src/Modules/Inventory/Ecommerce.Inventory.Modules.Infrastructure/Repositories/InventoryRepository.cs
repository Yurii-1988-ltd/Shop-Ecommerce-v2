using Microsoft.EntityFrameworkCore;

internal sealed class InventoryRepository
    : IInventoryRepository
{
    private readonly InventoryContext context;

    public InventoryRepository(InventoryContext context)
    {
        this.context = context;
    }

    public async Task<InventoryItem?> GetAsync(
        Guid inventoryItemId,
        CancellationToken cancellationToken = default)
    {
        return await context.Inventories
            .FirstOrDefaultAsync(
                x => x.Id == inventoryItemId,
                cancellationToken);
    }

    public async Task AddAsync(
        InventoryItem inventoryItem,
        CancellationToken cancellationToken = default)
    {
        await context.Inventories.AddAsync(
            inventoryItem,
            cancellationToken);
    }
}