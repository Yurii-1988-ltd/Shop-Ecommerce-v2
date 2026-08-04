

namespace Ecommerce.Order.Modules.Application.Features.RemoveOrderItem;

public sealed record RemoveOrderItemCommand(Guid OrderId,Guid  OrderItemId) : ICommand;

