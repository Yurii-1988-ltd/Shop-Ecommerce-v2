
using Ecommerce.Domain.Domain;
using Ecommerce.Domain.ValueObjects;
using Ecommerce.Promotions.Modules.Domain.Errors;
using Ecommerce.Promotions.Modules.Domain.ValueObjects;

namespace Ecommerce.Promotions.Modules.Domain.Entities;

public abstract class Coupon: Entity
{
    public CouponCode Code { get; init; }
    public DateTime ExpirationDateUtc { get; init; }
    public Money MinimumSpend { get; init; }
    protected Coupon()
    {

    }

    protected Coupon(Guid id, CouponCode code, DateTime expirationDateUtc, Money minimumSpend)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        Code = code;
        ExpirationDateUtc = expirationDateUtc;
        MinimumSpend = minimumSpend;
    }

 
    protected static Result ValidateBase(string code, Money minimumSpend)
    {
        if (string.IsNullOrWhiteSpace(code))
            return CouponErrors.CouponCodeIsEmpty;

        if (minimumSpend is null)
            return CouponErrors.NullMinimumSpend;

        return Result.Success();
    }

    public bool IsValid(DateTime currentDateUtc) => ExpirationDateUtc >= currentDateUtc;
    public bool IsSatisfiedBy(Money subtotal)
    {
        // Защита от NRE
        if (MinimumSpend is null || subtotal is null)
            return false;

        return subtotal.Amount >= MinimumSpend.Amount;
    }
    public abstract Money CalculateDiscount(Money subtotal);
}
