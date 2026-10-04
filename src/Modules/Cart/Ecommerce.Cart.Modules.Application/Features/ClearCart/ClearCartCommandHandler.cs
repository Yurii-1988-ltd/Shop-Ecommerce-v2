

namespace Ecommerce.Cart.Modules.Application.Features.ClearCart;

internal sealed class ClearCartCommandHandler(ICartRepository repository): ICommandHandler<ClearCartCommand>
{
    public async Task<Result> Handle(ClearCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await repository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);
        if (cart is null)
            return CartErrors.NotFound(request.CustomerId);
        cart.Clear();
        await repository.UpdateAsync(cart, cancellationToken);
        return Result.Success();
    }
}