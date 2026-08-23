using Ecommerce.Cart.Modules.Domain.Errors;
using Ecommerce.Cart.Modules.Domain.ValueObjects;
using Ecommerce.Domain.Domain;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Cart.Modules.Domain.Entities;

public class FixedAmountCoupon : Coupon
{
    public Money DiscountAmount { get; private set; }

    protected FixedAmountCoupon() { } // EF Core / Mongo

    private FixedAmountCoupon(
        Guid id,
        CouponCode code,
        DateTime expirationDateUtc,
        Money minimumSpend,
        Money discountAmount)
        : base(id, code, expirationDateUtc, minimumSpend)
    {
        DiscountAmount = discountAmount;
    }

    public static Result<FixedAmountCoupon> Create(
        CouponCode code,
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

        if (minimumSpend.Currency != discountAmount.Currency)
            return CouponErrors.CurrencyMismatch;

        var couponId = id ?? Guid.NewGuid();

        // Передаем параметр code (CouponCode) напрямую
        var coupon = new FixedAmountCoupon(
            couponId,
            code,
            expirationDateUtc,
            minimumSpend,
            discountAmount);

        return Result.Success(coupon);
    }

    public override Money CalculateDiscount(Money subtotal)
    {
        if (!IsSatisfiedBy(subtotal))
            return Money.Create(0, subtotal.Currency).Value;

        var discount = Math.Min(subtotal.Amount, DiscountAmount.Amount);

        return Money.Create(discount, subtotal.Currency).Value;
    }
}