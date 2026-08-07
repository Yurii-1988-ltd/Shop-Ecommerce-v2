using Ecommerce.Domain.Domain;

namespace Ecommerce.Inventory.Modules.Domain.Events;

public sealed class ReservationCommittedDomainEvent(Guid InventoryItemId, Guid ProductId, int ReservedQuantity) : DomainEvent
{
    public Guid InventoryItemId { get; set; } = InventoryItemId;
    public Guid ProductId { get; set; } = ProductId;
    public int ReservedQuantity { get; set; } = ReservedQuantity;
}