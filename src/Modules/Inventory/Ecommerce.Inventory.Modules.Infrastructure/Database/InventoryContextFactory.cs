using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ecommerce.Inventory.Modules.Infrastructure.Database;

internal sealed class InventoryContextFactory
    : IDesignTimeDbContextFactory<InventoryContext>
{
    public InventoryContext CreateDbContext(string[] args)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<InventoryContext>();

        optionsBuilder.UseNpgsql(
     "Host=localhost;Port=5432;Database=inventories;Username=postgres;Password=postgres");

        return new InventoryContext(optionsBuilder.Options);
    }
}