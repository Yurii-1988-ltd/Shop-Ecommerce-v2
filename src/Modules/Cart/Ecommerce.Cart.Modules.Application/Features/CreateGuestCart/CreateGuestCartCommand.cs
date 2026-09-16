
namespace Ecommerce.Cart.Modules.Application.Features.CreateGuestCart;

public record CreateGuestCartCommand(Guid GuestId) : ICommand<Guid>;

