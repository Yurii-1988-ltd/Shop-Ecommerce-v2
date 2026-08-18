using Ecommerce.Application.CQRS;
using Ecommerce.Basket.Modules.Domain.Entities;
using Ecommerce.Basket.Modules.Domain.Repository;
using Ecommerce.Domain.Domain;

namespace Ecommerce.Basket.Modules.Application.Features.AddItemToBasket;

public sealed class AddItemToBasketCommandHandler(IBasketRepository repository)
    : ICommandHandler<AddItemToBasketCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        AddItemToBasketCommand request,
        CancellationToken cancellationToken)
    {
       
        var basketResult = await repository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);

       Domain.Entities. Basket basket;

        if (basketResult.IsFailure)
        {
       
            var createBasketResult = Domain.Entities. Basket.Create(request.CustomerId);
            if (createBasketResult.IsFailure)
            {
                return Result.Failure<Guid>(createBasketResult.Error);
            }

            basket = createBasketResult.Value;

  
            await repository.AddAsync(basket, cancellationToken);
        }
        else
        {
            basket = basketResult.Value;
        }

    
        var addItemResult = basket.AddOrUpdateItem(
            request.ProductId,
            request.ProductName,
            request.UnitPrice,
            request.Quantity);

        if (addItemResult.IsFailure)
        {
            return Result.Failure<Guid>(addItemResult.Error);
        }

  
        var updateResult = await repository.UpdateAsync(basket, cancellationToken);
        if (updateResult.IsFailure)
        {
            return Result.Failure<Guid>(updateResult.Error);
        }

        return Result.Success(basket.Id);
    }
}