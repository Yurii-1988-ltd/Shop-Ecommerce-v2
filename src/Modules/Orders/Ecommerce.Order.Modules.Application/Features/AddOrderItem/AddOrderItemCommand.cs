
using Ecommerce.Application.CQRS;

namespace Ecommerce.Order.Modules.Application.Features.AddOrderItem;

public sealed record AddOrderItemCommand(
   Guid OrderId,
    Guid ProductId,
 
    int Quantity) : ICommand;

