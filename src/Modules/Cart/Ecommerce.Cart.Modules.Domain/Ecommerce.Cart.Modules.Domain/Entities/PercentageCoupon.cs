
using Ecommerce.Cart.Modules.Domain.Entities;
using Ecommerce.Cart.Modules.Domain.Errors;
using Ecommerce.Domain.Domain;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Cart.Modules.Domain.Entities;

public sealed class PercentageCoupon : Coupon
{
    public decimal Percentage { get; private set; }
    public Money? MaxDiscountAmount { get; private set; }

    private PercentageCoupon()
    {
    }

    private PercentageCoupon(
        Guid id,
        string code,
        DateTime expirationDateUtc,
        Money minimumSpend,
        decimal percentage,
        Money? maxDiscountAmount)
        : base(
            id,
            code,
            expirationDateUtc,
            minimumSpend)
    {
        Percentage = percentage;
        MaxDiscountAmount = maxDiscountAmount;
    }

    public static Result<PercentageCoupon> Create(
        string code,
        DateTime expirationDateUtc,
        Money minimumSpend,
        decimal percentage,
        Money? maxDiscountAmount = null,
        Guid? id = null)
    {
        var baseResult = ValidateBase(code, minimumSpend);

        if (baseResult.IsFailure)
            return baseResult.Error;

        if (percentage <= 0 || percentage > 1)
            return CouponErrors.InvalidPercentage;

        if (maxDiscountAmount is not null)
        {
            if (maxDiscountAmount.Amount <= 0)
                return CouponErrors.InvalidMaxDiscountAmount;

            if (maxDiscountAmount.Currency != minimumSpend.Currency)
                return CouponErrors.CurrencyMismatch;
        }

        var couponId = id ?? Guid.NewGuid();

        var formattedCode = code
            .Trim()
            .ToUpperInvariant();

        return Result.Success(
            new PercentageCoupon(
                couponId,
                formattedCode,
                expirationDateUtc,
                minimumSpend,
                percentage,
                maxDiscountAmount));
    }

    public override Money CalculateDiscount(Money subtotal)
    {
        var discount = subtotal.Amount * Percentage;

        if (MaxDiscountAmount is not null)
        {
            discount = Math.Min(
                discount,
                MaxDiscountAmount.Amount);
        }

        return new Money(
            discount,
            subtotal.Currency);
    }
}