

namespace Ecommerce.Promotions.Modules.Infrastructure.Services;

public sealed class CouponValidationService(
    ICouponRepository couponRepository) : ICouponService
{
    public async Task<Result<DiscountResultDto>> ValidateAsync(
        string code,
        decimal subtotal,
        string currency,
        CancellationToken cancellationToken = default)
    {
        // 1. Проверка входных данных
        if (string.IsNullOrWhiteSpace(code))
            return PromotionErrors.CouponCodeIsEmpty;

        if (subtotal < 0)
            return PromotionErrors.InvalidSubtotal;

        // 2. Создание Value Object кода купона
        var couponCodeResult = CouponCode.Create(code);
        if (couponCodeResult.IsFailure)
            return couponCodeResult.Error;

        // 3. Поиск купона в БД
        var coupon = await couponRepository.GetByCodeAsync(
            couponCodeResult.Value,
            cancellationToken);

        if (coupon is null)
            return PromotionErrors.CouponNotFound(code);

        // 4. Доменные проверки состояния купона
        var currentDateUtc = DateTime.UtcNow;

        if (!coupon.IsValid(currentDateUtc))
            return PromotionErrors.CouponExpired;

        // 5. Проверка минимальной суммы заказа (MinimumSpend)
        if (coupon.MinimumSpend is not null)
        {
            if (coupon.MinimumSpend.Currency != currency)
                return MoneyErrors.CurrencyMismatch;

            if (subtotal < coupon.MinimumSpend.Amount)
                return PromotionErrors.MinimumSpendNotReached(coupon.MinimumSpend.Amount);
        }

        // 6. Расчет суммы скидки
        var subtotalMoneyResult = Money.Create(subtotal, currency);
        if (subtotalMoneyResult.IsFailure)
            return subtotalMoneyResult.Error;

        var discountMoney = coupon.CalculateDiscount(subtotalMoneyResult.Value);

        // 7. Формирование результата
        var resultDto = new DiscountResultDto(
            Discount: discountMoney,
            CouponCode: coupon.Code.Value
        );

        return Result.Success(resultDto);
    }
}