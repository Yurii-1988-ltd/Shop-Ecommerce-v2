using Ecommerce.Cart.Modules.Domain.Errors;
using Ecommerce.Domain.Constants;
using Ecommerce.Domain.Domain;
using Ecommerce.Domain.Errors;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Cart.Modules.Domain.Entities;

public sealed class Cart : Entity
{
    private  List<CartItem> _items = new();

    public Guid CustomerId { get; private set; }
    public Coupon? AppliedCoupon { get; private set; }
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

    private Cart() { }

    public Cart(Guid id, Guid customerId)
    {
        Id = id;
        CustomerId = customerId;
    }

    public static Result<Cart> Create(Guid customerId)
    {
        if (customerId == Guid.Empty)
            return CartErrors.InvalidCustomerId;

        return new Cart(Guid.NewGuid(), customerId);
    }

    public Result AddItem(Guid productId, string name, Money price, int quantity)
    {
        var firstItem = _items.FirstOrDefault();
        if (firstItem != null && firstItem.Currency != price.Currency)
        {
            return MoneyErrors.CurrencyMismatch;
        }

        var existingItem = _items.SingleOrDefault(x => x.ProductId == productId);
        if (existingItem != null)
        {
            return existingItem.UpdateQuantity(existingItem.Quantity + quantity);
        }

        var itemResult = CartItem.Create(productId, name, price, quantity);
        if (itemResult.IsFailure)
            return Result.Failure(itemResult.Error);

        _items.Add(itemResult.Value);
        return Result.Success();
    }

    public Result RemoveItem(Guid productId)
    {
        var item = _items.SingleOrDefault(y => y.ProductId == productId);
        if (item == null)
        {
            return CartItemErrors.NotFound(productId);
        }

        _items.Remove(item);

        // Если сумма упала ниже порога скидки — сбрасываем купон
        var subtotalResult = GetSubtotal();
        if (subtotalResult.IsSuccess && AppliedCoupon != null && !AppliedCoupon.IsSatisfiedBy(subtotalResult.Value))
        {
            RemoveCoupon();
        }

        return Result.Success();
    }

    public Result ChangeQuantity(Guid productId, int quantity)
    {
        if (quantity == 0)
            return RemoveItem(productId);

        var item = _items.SingleOrDefault(x => x.ProductId == productId);
        if (item is null)
            return CartItemErrors.NotFound(productId);

        var updateResult = item.UpdateQuantity(quantity);
        if (updateResult.IsFailure)
            return updateResult;

        // Проверяем актуальность купона при уменьшении количества
        var subtotalResult = GetSubtotal();
        if (subtotalResult.IsSuccess && AppliedCoupon != null && !AppliedCoupon.IsSatisfiedBy(subtotalResult.Value))
        {
            RemoveCoupon();
        }

        return Result.Success();
    }

    public void Clear()
    {
        _items.Clear();
        AppliedCoupon = null;
    }

    // --- Domain Actions for Coupons ---
    public Result ApplyCoupon(Coupon coupon, DateTime currentDateUtc)
    {

        if (!coupon.IsValid(currentDateUtc))
            return CouponErrors.CouponExpired;

        var subtotalResult = GetSubtotal();
        if (subtotalResult.IsFailure)
            return subtotalResult.Error;

        if (!coupon.IsSatisfiedBy(subtotalResult.Value))
            return CouponErrors.CouponMinimumSpendNotMet;

        AppliedCoupon = coupon;
        return Result.Success();
    }

    public Result RemoveCoupon()
    {
        AppliedCoupon = null;
        return Result.Success();
    }

    public Result<Money> GetSubtotal()
    {
       

        if (!_items.Any())
            return Money.Create(0, CurrencyConstant.UAH);

        var currency = _items[0].Currency;
        var totalAmount = _items.Sum(x => x.TotalPrice.Amount);


        return Money.Create(totalAmount, currency);
    }

    public Result<Money> GetDiscountTotal()
    {
        var subtotalResult = GetSubtotal();
        if (subtotalResult.IsFailure) return subtotalResult.Error;

        if (AppliedCoupon is null)
            return Money.Create(0, subtotalResult.Value.Currency);

        return AppliedCoupon.CalculateDiscount(subtotalResult.Value);
    }

    public Result<Money> GetTotalCost()
    {
        var subtotalResult = GetSubtotal();
        if (subtotalResult.IsFailure) return subtotalResult.Error;

        var discountResult = GetDiscountTotal();
        if (discountResult.IsFailure) return discountResult.Error;

        var finalAmount = Math.Max(0, subtotalResult.Value.Amount - discountResult.Value.Amount);
        return Money.Create(finalAmount, subtotalResult.Value.Currency);
    }
}