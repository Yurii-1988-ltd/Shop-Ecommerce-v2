

using Ecommerce.Order.Modules.Domain.Enums;

namespace Ecommerce.Order.Modules.Application.Features.ForceChangeStatus;

public record ForceChangeStatusCommand(Guid OrderId, OrderStatus NewStatus, Guid ChangedBy, string Reason) : ICommand;

