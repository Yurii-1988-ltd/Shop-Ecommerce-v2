using Ecommerce.Application.CQRS;
using Ecommerce.Order.Modules.Application.Features.Responses;

namespace Ecommerce.Order.Modules.Application.Features.GetOrder;

public sealed record GetOrderQuery(
    Guid OrderId,
    Guid? CustomerId = null,
    Guid? GuestId = null) : IQuery<OrderResponse>;