

using Ecommerce.Domain.Domain;

namespace Ecommerce.Inventory.Modules.Domain.Events;

public class StockDeductedDomainEvent(Guid Id, Guid ProductId, int Quantity) : DomainEvent
{
    public Guid Id { get; set; } = Id;
    public Guid ProductId { get; set; } = ProductId;
    public int Quantity { get; set; } = Quantity;

}
