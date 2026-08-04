using Ecommerce.Application.CQRS;
using Ecommerce.Cart.Modules.Domain.Errors;
using Ecommerce.Cart.Modules.Domain.Repositories;
using Ecommerce.Domain.Domain;

namespace Ecommerce.Cart.Modules.Application.Features.RemoveCart;

internal sealed class RemoveCartCommandHandler(ICartRepository repository): ICommandHandler<RemoveCartCommand>
{
    public async Task<Result> Handle(RemoveCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await repository.GetByCustomerIdAsync(request.CustomerId);
        if (cart is null)
            return CartErrors.NotFound(request.CustomerId);
        await repository.DeleteAsync(cart.Id, cancellationToken);
        return Result.Success();

    }
}