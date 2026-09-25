namespace Ecommerce.Inventory.Modules.Application.Abstractions;

public interface IInventoryUnitOfWork
{
    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);
}