

using Ecommerce.Application.CQRS;
using Ecommerce.Basket.Modules.Application.Mapping;
using Ecommerce.Basket.Modules.Application.Responses;
using Ecommerce.Basket.Modules.Domain.Repository;
using Ecommerce.Domain.Domain;

namespace Ecommerce.Basket.Modules.Application.Features.GetBasketByCustomer;

internal sealed class GetBasketByCustomerIdQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasketByCustomerIdQuery, BasketResponse>
{
    public async Task<Result<BasketResponse>> Handle(GetBasketByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        var  basketResult = await repository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);
        if (basketResult.IsFailure)
        {
            return Result.Failure<BasketResponse>(basketResult.Error);
        }
        return Result.Success(basketResult.Value.ToResponse());

    }
}
