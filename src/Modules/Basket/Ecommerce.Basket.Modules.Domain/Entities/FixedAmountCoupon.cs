using Ecommerce.Basket.Modules.Domain.Errors;
using Ecommerce.Domain.Domain;
using Ecommerce.Domain.ValueObjects;

public class FixedAmountCoupon : Coupon
{
    public Money DiscountAmount { get; private set; }

    private FixedAmountCoupon() { } // EF Core

    private FixedAmountCoupon(Guid id, string code, DateTime expirationDateUtc, Money minimumSpend, Money discountAmount)
        : base(id, code, expirationDateUtc, minimumSpend)
    {
        DiscountAmount = discountAmount;
    }

    public static Result<FixedAmountCoupon> Create(
        string code,
        DateTime expirationDateUtc,
        Money minimumSpend,
        Money discountAmount,
        Guid? id = null)
    {
        var baseResult = ValidateBase(code, minimumSpend);
        if (baseResult.IsFailure)
            return baseResult.Error;

        if (discountAmount is null || discountAmount.Amount <= 0)
            return CouponErrors.InvalidDiscountAmount;

        var couponId = id ?? Guid.NewGuid();
        var formattedCode = code.Trim().ToUpperInvariant();

        var coupon = new FixedAmountCoupon(couponId, formattedCode, expirationDateUtc, minimumSpend, discountAmount);



        return Result.Success(coupon);
    }

    public override Money CalculateDiscount(Money subtotal)
    {
        if (!IsSatisfiedBy(subtotal)) return Money.Zero(subtotal.Currency);
        var discount = Math.Min(subtotal.Amount, DiscountAmount.Amount);
        return new Money(discount, subtotal.Currency);
    }
}