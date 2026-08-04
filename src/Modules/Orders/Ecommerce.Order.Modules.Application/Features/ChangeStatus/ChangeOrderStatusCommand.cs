
using Ecommerce.Order.Modules.Domain.Enums;

namespace Ecommerce.Order.Modules.Application.Features.ChangeStatus;

public sealed record ChangeOrderStatusCommand(Guid OrderId, OrderStatus OrderStatus) : ICommand;

