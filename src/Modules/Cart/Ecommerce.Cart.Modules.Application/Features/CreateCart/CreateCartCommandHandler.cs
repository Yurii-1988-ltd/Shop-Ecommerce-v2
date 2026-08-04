

using Ecommerce.Application.CQRS;
using Ecommerce.Cart.Modules.Domain.Errors;
using Ecommerce.Cart.Modules.Domain.Repositories;
using Ecommerce.Domain.Domain;

namespace Ecommerce.Cart.Modules.Application.Features.CreateCart;

internal sealed class CreateCartCommandHandler(
    ICartRepository cartRepository)
    : ICommandHandler<CreateCartCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateCartCommand request,
        CancellationToken cancellationToken)
    {
        var existing = await cartRepository.GetByCustomerIdAsync(
            request.CustomerId,
            cancellationToken);

        if (existing is not null)
            return CartErrors.AlreadyExists(request.CustomerId);

        var cartResult = Domain.Entities.Cart.Create(request.CustomerId);

        if (cartResult.IsFailure)
            return cartResult.Error;

        await cartRepository.InsertAsync(
            cartResult.Value,
            cancellationToken);

        return cartResult.Value.Id;
    }
}
