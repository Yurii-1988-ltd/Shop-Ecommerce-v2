using Ecommerce.Domain.Domain;

namespace Ecommerce.Inventory.Modules.Domain.Events;

public sealed class StockReservedDomainEvent(Guid inventoryItemId, Guid productId, int quantity) : DomainEvent
{
    public Guid InventoryItemId { get; set; } = inventoryItemId;
    public Guid ProductId { get; set; } = productId;
    public int Quantity { get; set; } = quantity;
    
}
