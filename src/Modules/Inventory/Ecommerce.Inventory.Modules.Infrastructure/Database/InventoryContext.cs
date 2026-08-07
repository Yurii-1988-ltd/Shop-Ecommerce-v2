
using Ecommerce.Application.Abstractions;
using Ecommerce.Inventory.Modules.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Inventory.Modules.Infrastructure.Database;

internal sealed class InventoryContext : DbContext, IUnitOfWork
{
    public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
    {
        
    }
    public DbSet<InventoryItem> Inventories { get; set; } = null!;

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryContext).Assembly);
    }
}
