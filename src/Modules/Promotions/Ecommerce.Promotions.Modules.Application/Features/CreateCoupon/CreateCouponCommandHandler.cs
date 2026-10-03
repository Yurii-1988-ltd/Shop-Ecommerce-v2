


namespace Ecommerce.Promotions.Modules.Application.Features.CreateCoupon;

public sealed class CreateCouponCommandHandler(
    ICouponRepository couponRepository)
    : ICommandHandler<CreateCouponCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateCouponCommand request,
        CancellationToken cancellationToken)
    {
        var codeResult = CouponCode.Create(request.Code);

        if (codeResult.IsFailure)
            return codeResult.Error;

        var existingCoupon = await couponRepository.GetByCodeAsync(
            codeResult.Value,
            cancellationToken);

        if (existingCoupon is not null)
            return CouponErrors.AlreadyExists(request.Code);

        var minSpendResult = Money.Create(
            request.MinimumSpend,
            request.Currency);

        if (minSpendResult.IsFailure)
            return minSpendResult.Error;

        Coupon coupon;

        if (request.Type == CouponType.Fixed)
        {
            var discountResult = Money.Create(
                request.AmountOrPercentage,
                request.Currency);

            if (discountResult.IsFailure)
                return discountResult.Error;

            var fixedResult = FixedAmountCoupon.Create(
                codeResult.Value,
                request.ExpirationDateUtc,
                minSpendResult.Value,
                discountResult.Value);

            if (fixedResult.IsFailure)
                return fixedResult.Error;

            coupon = fixedResult.Value;
        }
        else if (request.Type == CouponType.Percentage)
        {
            Money? maxDiscount = null;

            if (request.MaxDiscountAmount.HasValue)
            {
                var maxDiscountResult = Money.Create(
                    request.MaxDiscountAmount.Value,
                    request.Currency);

                if (maxDiscountResult.IsFailure)
                    return maxDiscountResult.Error;

                maxDiscount = maxDiscountResult.Value;
            }

            var percentageResult = PercentageCoupon.Create(
                codeResult.Value,
                request.ExpirationDateUtc,
                minSpendResult.Value,
                request.AmountOrPercentage,
                maxDiscount);

            if (percentageResult.IsFailure)
                return percentageResult.Error;

            coupon = percentageResult.Value;
        }
        else
        {
            return CouponErrors.InvalidType;
        }

        await couponRepository.AddAsync(
            coupon,
            cancellationToken);

        return coupon.Id;
    }
}