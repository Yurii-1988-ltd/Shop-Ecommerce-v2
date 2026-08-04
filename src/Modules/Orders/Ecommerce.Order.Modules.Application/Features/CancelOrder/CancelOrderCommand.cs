

namespace Ecommerce.Order.Modules.Application.Features.CancelOrder;

public record CancelOrderCommand(Guid OrderId) : ICommand;

