
namespace Ecommerce.Order.Modules.Domain.Events;

internal sealed class OrderCancelledDomainEvent(Guid orderId): DomainEvent
{
    public Guid OrderId { get; } = orderId;
 
  
}

