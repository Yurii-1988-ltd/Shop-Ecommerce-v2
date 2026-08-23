using Ecommerce.Cart.Modules.Application.Features.CreateCoupon;
using Ecommerce.Cart.Modules.Domain.Entities;
using Ecommerce.Cart.Modules.Domain.Errors;
using Ecommerce.Cart.Modules.Domain.Repositories;
using Ecommerce.Cart.Modules.Domain.ValueObjects;
using Ecommerce.Domain.Domain;
using Ecommerce.Domain.ValueObjects;
using MediatR;


public sealed class CreateCouponCommandHandler(ICouponRepository couponRepository)
    : IRequestHandler<CreateCouponCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
    {
        // 1. Создаем и валидируем CouponCode
        var codeResult = CouponCode.Create(request.Code);
        if (codeResult.IsFailure)
            return codeResult.Error;

        // 2. Проверяем уникальность кода
        var existingCoupon = await couponRepository.GetByCodeAsync(codeResult.Value, cancellationToken);
        if (existingCoupon is not null)
            return CouponErrors.AlreadyExists(request.Code);

        // 3. Создаем Money Value Objects
        var minSpendResult = Money.Create(request.MinimumSpend, request.Currency);
        if (minSpendResult.IsFailure) return minSpendResult.Error;

        Coupon coupon;

        if (request.Type.Equals("Fixed", StringComparison.OrdinalIgnoreCase))
        {
            var discountResult = Money.Create(request.AmountOrPercentage, request.Currency);
            if (discountResult.IsFailure) return discountResult.Error;

            var fixedResult = FixedAmountCoupon.Create(
                codeResult.Value,
                request.ExpirationDateUtc,
                minSpendResult.Value,
                discountResult.Value);

            if (fixedResult.IsFailure) return fixedResult.Error;
            coupon = fixedResult.Value;
        }
        else
        {
            Money? maxDiscount = null;
            if (request.MaxDiscountAmount.HasValue)
            {
                var maxRes = Money.Create(request.MaxDiscountAmount.Value, request.Currency);
                if (maxRes.IsFailure) return maxRes.Error;
                maxDiscount = maxRes.Value;
            }

            var percentResult = PercentageCoupon.Create(
                codeResult.Value,
                request.ExpirationDateUtc,
                minSpendResult.Value,
                request.AmountOrPercentage,
                maxDiscount);

            if (percentResult.IsFailure) return percentResult.Error;
            coupon = percentResult.Value;
        }

        await couponRepository.AddAsync(coupon, cancellationToken);
        return coupon.Id;
    }
}