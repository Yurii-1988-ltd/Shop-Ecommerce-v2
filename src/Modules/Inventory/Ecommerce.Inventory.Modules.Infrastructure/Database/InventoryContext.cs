public sealed class InventoryContext : DbContext, IUnitOfWork
{
    public InventoryContext(
        DbContextOptions<InventoryContext> options)
        : base(options)
    {
    }

    public DbSet<InventoryItem> Inventories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(
            new InventoryItemConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}