using Ecommerce.Application.CQRS;
using Ecommerce.Cart.Modules.Domain.Errors;
using Ecommerce.Cart.Modules.Domain.Repositories;
using Ecommerce.Domain.Domain;

namespace Ecommerce.Cart.Modules.Application.Features.ClearCart;

internal sealed class ClearCartCommandHandler(ICartRepository repository): ICommandHandler<ClearCartCommand>
{
    public async Task<Result> Handle(ClearCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await repository.GetByCustomerIdAsync(request.CustomerId);
        if (cart is null)
            return CartItemErrors.NotFound(request.CustomerId);
        cart.Clear();
        await repository.UpdateAsync(cart, cancellationToken);
        return Result.Success();
    }
}