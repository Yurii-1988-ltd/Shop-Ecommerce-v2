

namespace Ecommerce.Order.Modules.Domain.Events;

internal sealed class OrderSubmittedDomainEvent(Guid  orderId) : DomainEvent
{
    public Guid OrderId { get;  } = orderId;

}
