

using Ecommerce.Domain.Domain;

namespace Ecommerce.Cart.Modules.Domain.Errors;

public static class CouponErrors
{
    public static Error CouponCodeIsEmpty
        => new Error("CouponCode.IsEmpty ", "Coupon code can not be empty", ErrorType.Validation);
    public static Error NullMinimumSpend
        => new Error("Null.Minimum.Spend", "The minimum spend requirement for this coupon is not configured.", ErrorType.Validation);
    public static Error InvalidDiscountAmount
        => new Error("Invalid.Discount.Amount", "Diiscount invalid amount", ErrorType.Validation);

    public static  Error InvalidPercentage =>
    new("Coupon.Percentage.Invalid", "Coupon percentage must be greater than 0 and less than or equal to 100%.",ErrorType.Validation);

    public static  Error InvalidMaxDiscountAmount =
        new("Coupon.MaxDiscountAmount.Invalid", "Maximum discount amount must be greater than zero.",ErrorType.Validation);

    public static  Error CurrencyMismatch =
        new("Coupon.Currency.Mismatch", "Coupon monetary values must use the same currency.",ErrorType.Validation);
}
