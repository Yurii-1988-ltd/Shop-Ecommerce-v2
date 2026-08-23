using Ecommerce.Application.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Cart.Modules.Application.Features.CreateCoupon
{
    public sealed record CreateCouponCommand(
     string Code,
    string Type,
    decimal AmountOrPercentage,
    decimal MinimumSpend,
    string Currency,
    DateTime ExpirationDateUtc,
    decimal? MaxDiscountAmount = null) : ICommand<Guid>;
   
}
