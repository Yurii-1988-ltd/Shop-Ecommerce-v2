using Ecommerce.Domain.Domain;

namespace Ecommerce.Inventory.Modules.Domain.Events;

public sealed class StockReplenishedDomainEvent(Guid InventoryItemId, Guid ProductId, int ReplenishedQuantity) : DomainEvent
{
    public Guid InventoryItemId { get; set; } = InventoryItemId;
    public Guid ProductId { get; set; } = ProductId;
    public int ReplenishedQuantity { get; set; } = ReplenishedQuantity;
}