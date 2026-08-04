using System.Windows.Input;
using ICommand = Ecommerce.Application.CQRS.ICommand;

namespace Ecommerce.Cart.Modules.Application.Features.ClearCart;

public  sealed  record ClearCartCommand(Guid CustomerId): ICommand;