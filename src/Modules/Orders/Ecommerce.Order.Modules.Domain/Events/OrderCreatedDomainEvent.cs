
using Ecommerce.Domain.Domain;

namespace Ecommerce.Order.Modules.Domain.Events;

internal sealed class OrderCreatedDomainEvent : DomainEvent
{
    public Guid OrderId { get;  }
    public Guid CustomerId { get;  }
    public OrderCreatedDomainEvent(Guid orderId,Guid customerId)
    {
        OrderId = orderId;
        CustomerId = customerId;
        
    }
}
