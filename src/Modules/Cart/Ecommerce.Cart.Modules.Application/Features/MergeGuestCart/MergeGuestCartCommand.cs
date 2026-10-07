

namespace Ecommerce.Cart.Modules.Application.Features.MergeGuestCart;

public sealed record MergeGuestCartCommand(Guid GuestId, Guid CustomerId) : ICommand;

