using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Order.Modules.Domain.Events;



internal sealed class OrderItemRemovedDomainEvent(
    Guid orderId,
    Guid orderItemId,
    Guid productId) : DomainEvent
{
    public Guid OrderId { get; } = orderId;

    public Guid OrderItemId { get; } = orderItemId;

    public Guid ProductId { get; } = productId;
}
