using Ecommerce.Application.CQRS;
using Ecommerce.Basket.Modules.Application.Responses;

namespace Ecommerce.Basket.Modules.Application.Features.GetBasketByCustomer
{
    public sealed record GetBasketByCustomerIdQuery(Guid CustomerId) : IQuery<BasketResponse>;
   
}
