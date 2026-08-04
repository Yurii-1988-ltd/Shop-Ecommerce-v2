

using Ecommerce.Order.Modules.Domain.ValueObjects;

namespace Ecommerce.Order.Modules.Application.Features.UpdateShippingAddress;

public sealed record UpdateShippingAddressCommand(Guid OrderId, OrderAddress ShippingAddress) : ICommand;

