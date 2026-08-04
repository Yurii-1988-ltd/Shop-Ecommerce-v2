

namespace Ecommerce.Order.Modules.Application.Features.SubmitOrder;

public sealed record SubmitOrderCommand(Guid OrderId) : ICommand;

