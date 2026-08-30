namespace Ecommerce.Admin.ApiClients.Coupons.Contracts;

public sealed record UpdateCouponRequest(
    Guid Id,
    string Code,
    string Type,
    decimal AmountOrPercentage,
    decimal MinimumSpend,
    string Currency,
    DateTime ExpirationDateUtc,
    decimal? MaxDiscountAmount = null);
