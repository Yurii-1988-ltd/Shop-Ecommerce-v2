

namespace Ecommerce.Promotions.Modules.Application.Features.UpdateCoupon;

public sealed record UpdateCouponCommand(
    Guid Id,
    string Code,
    CouponType Type,
    decimal AmountOrPercentage,
    decimal MinimumSpend,
    string Currency,
    DateTime ExpirationDateUtc,
    decimal? MaximumDiscountAmount) : ICommand;

