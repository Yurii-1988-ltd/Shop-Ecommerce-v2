



namespace Ecommerce.Inventory.Modules.Infrastructure.Database;

internal sealed class InventoryUnitOfWork(
    InventoryContext context) : IInventoryUnitOfWork
{
    public Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
        => context.SaveChangesAsync(cancellationToken);
}
