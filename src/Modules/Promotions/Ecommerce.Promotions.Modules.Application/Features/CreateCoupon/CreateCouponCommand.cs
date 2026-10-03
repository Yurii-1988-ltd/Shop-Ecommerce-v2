


namespace Ecommerce.Promotions.Modules.Application.Features.CreateCoupon
{
    public sealed record CreateCouponCommand(
     string Code,
    CouponType Type,
    decimal AmountOrPercentage,
    decimal MinimumSpend,
    string Currency,
    DateTime ExpirationDateUtc,
    decimal? MaxDiscountAmount = null) : ICommand<Guid>;
   
}
