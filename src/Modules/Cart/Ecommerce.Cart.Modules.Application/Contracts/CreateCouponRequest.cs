namespace Ecommerce.Cart.Modules.Application.Contracts;



public record CreateCouponRequest(
    string Code,
    string Type,
    decimal AmountOrPercentage,
    decimal MinimumSpend,
    string Currency,
    DateTime ExpirationDateUtc,
    decimal? MaxDiscountAmount = null);
