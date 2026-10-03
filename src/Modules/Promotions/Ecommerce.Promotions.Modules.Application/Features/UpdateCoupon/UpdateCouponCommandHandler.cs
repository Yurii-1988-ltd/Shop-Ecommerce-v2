
namespace Ecommerce.Promotions.Modules.Application.Features.UpdateCoupon;

public sealed class UpdateCouponCommandHandler(
    ICouponRepository couponRepository)
    : ICommandHandler<UpdateCouponCommand>
{
    public async Task<Result> Handle(
        UpdateCouponCommand request,
        CancellationToken cancellationToken)
    {
        var existingCoupon = await couponRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (existingCoupon is null)
            return CouponErrors.NotFound(request.Id);

        var codeResult = CouponCode.Create(request.Code);

        if (codeResult.IsFailure)
            return codeResult.Error;

        var existingByCode = await couponRepository.GetByCodeAsync(
            codeResult.Value,
            cancellationToken);

        if (existingByCode is not null &&
            existingByCode.Id != existingCoupon.Id)
        {
            return CouponErrors.AlreadyExists(request.Code);
        }

        var minSpendResult = Money.Create(
            request.MinimumSpend,
            request.Currency);

        if (minSpendResult.IsFailure)
            return minSpendResult.Error;

        Coupon updatedCoupon;

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
                discountResult.Value,
                existingCoupon.Id);

            if (fixedResult.IsFailure)
                return fixedResult.Error;

            updatedCoupon = fixedResult.Value;
        }
        else if (request.Type == CouponType.Percentage)
        {
            Money? maxDiscount = null;

            if (request.MaximumDiscountAmount.HasValue)
            {
                var maxDiscountResult = Money.Create(
                    request.MaximumDiscountAmount.Value,
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
                maxDiscount,
                existingCoupon.Id);

            if (percentageResult.IsFailure)
                return percentageResult.Error;

            updatedCoupon = percentageResult.Value;
        }
        else
        {
            return CouponErrors.InvalidType;
        }

        await couponRepository.UpdateAsync(
            updatedCoupon,
            cancellationToken);

        return Result.Success();
    }
}