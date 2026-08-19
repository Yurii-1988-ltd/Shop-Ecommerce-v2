
using Ecommerce.Cart.Modules.Domain.Errors;
using Ecommerce.Domain.Domain;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Cart.Modules.Domain.Entities;
public abstract class  Coupon :Entity
{

    public string Code { get; }
    public DateTime ExpirationDateUtc { get; }
    public Money MinimumSpend { get; }
    protected Coupon()
    {
        
    }

    protected Coupon(Guid id, string code, DateTime expirationDateUtc, Money minimumSpend)
    {
        Id = id==Guid.Empty?Guid.NewGuid():id;
        Code = code;
        ExpirationDateUtc = expirationDateUtc;
        MinimumSpend = minimumSpend;
    }

    // Хелпер для валидации базовых полей в дочерних фабриках
    protected static Result ValidateBase(string code, Money minimumSpend)
    {
        if (string.IsNullOrWhiteSpace(code))
            return CouponErrors.CouponCodeIsEmpty;

        if (minimumSpend is null)
            return CouponErrors.NullMinimumSpend;

        return Result.Success();
    }

    public bool IsValid(DateTime currentDateUtc) => currentDateUtc <= ExpirationDateUtc;
    public bool IsSatisfiedBy(Money subtotal) => subtotal.Amount >= MinimumSpend.Amount;
    public abstract  Money CalculateDiscount(Money subtotal);
}