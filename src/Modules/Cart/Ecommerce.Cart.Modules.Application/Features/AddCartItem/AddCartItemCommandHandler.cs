using Ecommerce.Application.CQRS;
using Ecommerce.Cart.Modules.Domain.Errors;
using Ecommerce.Cart.Modules.Domain.Repositories;
using Ecommerce.Domain.Domain;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Cart.Modules.Application.Features.AddCartItem;

internal sealed class AddCartItemCommandHandler(ICartRepository repository): ICommandHandler<AddCartItemCommand>
{
    public async Task<Result> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
    {
        var cart = await repository.GetByCustomerIdAsync(
            request.CustomerId,
            cancellationToken);

        if (cart is null)
            return CartErrors.NotFound(request.CustomerId);

        var priceResult = Money.Create(request.Price, request.Currency);

        if (priceResult.IsFailure)
            return priceResult.Error;

        var result = cart.AddItem(
            request.ProductId,
            request.Name,
            priceResult.Value,
            request.Quantity);

        if (result.IsFailure)
            return result;

        var saved = await repository.GetByCustomerIdAsync(
    request.CustomerId,
    cancellationToken);
        Console.WriteLine($"After Save: {saved!.Items.Count}");

        await repository.UpdateAsync(cart, cancellationToken);

        return Result.Success();
     
    }
}