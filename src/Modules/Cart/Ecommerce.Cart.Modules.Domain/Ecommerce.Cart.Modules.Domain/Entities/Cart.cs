using Ecommerce.Cart.Modules.Domain.Errors;
using Ecommerce.Domain.Constants;
using Ecommerce.Domain.Domain;
using Ecommerce.Domain.Errors;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Cart.Modules.Domain.Entities;

public sealed class Cart : Entity
{
    private List<CartItem> _items = [];

    public Guid? CustomerId { get; private set; }

    public Guid? GuestId { get; private set; }

    public string? AppliedCouponCode { get; private set; }

    public Money? AppliedDiscount { get; private set; }

    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

    private Cart()
    {
    }

    private Cart(
        Guid id,
        Guid? customerId,
        Guid? guestId)
    {
        Id = id;
        CustomerId = customerId;
        GuestId = guestId;
    }

    public static Result<Cart> CreateForCustomer(Guid customerId)
    {
        if (customerId == Guid.Empty)
            return CartErrors.InvalidCustomerId;

        return new Cart(
            Guid.NewGuid(),
            customerId,
            null);
    }

    public static Result<Cart> CreateForGuest(Guid guestId)
    {
        if (guestId == Guid.Empty)
            return CartErrors.InvalidGuestId;

        return new Cart(
            Guid.NewGuid(),
            null,
            guestId);
    }

    public Result AddItem(
        Guid productId,
        string name,
        Money price,
        int quantity)
    {
        var firstItem = _items.FirstOrDefault();

        if (firstItem is not null &&
            firstItem.Currency != price.Currency)
        {
            return MoneyErrors.CurrencyMismatch;
        }

        var existingItem = _items.SingleOrDefault(
            x => x.ProductId == productId);

        if (existingItem is not null)
        {
            var updateResult = existingItem.UpdateQuantity(
                existingItem.Quantity + quantity);

            if (updateResult.IsFailure)
                return updateResult;

            ClearAppliedDiscount();

            return Result.Success();
        }

        var itemResult = CartItem.Create(
            productId,
            name,
            price,
            quantity);

        if (itemResult.IsFailure)
            return Result.Failure(itemResult.Error);

        _items.Add(itemResult.Value);

        ClearAppliedDiscount();

        return Result.Success();
    }

    public Result RemoveItem(Guid productId)
    {
        var item = _items.SingleOrDefault(
            x => x.ProductId == productId);

        if (item is null)
            return CartItemErrors.NotFound(productId);

        _items.Remove(item);

        ClearAppliedDiscount();

        return Result.Success();
    }

    public Result ChangeQuantity(Guid productId, int quantity)
    {
        if (quantity == 0)
            return RemoveItem(productId);

        var item = _items.SingleOrDefault(
            x => x.ProductId == productId);

        if (item is null)
            return CartItemErrors.NotFound(productId);

        var updateResult = item.UpdateQuantity(quantity);

        if (updateResult.IsFailure)
            return updateResult;

        ClearAppliedDiscount();

        return Result.Success();
    }

    public void Clear()
    {
        _items.Clear();
        ClearAppliedDiscount();
    }

    public Result ApplyCoupon(
        string couponCode,
        Money discount)
    {
        if (string.IsNullOrWhiteSpace(couponCode))
            return CartErrors.InvalidCouponCode;

        if (discount is null)
            return CartErrors.InvalidDiscount;

        var subtotalResult = GetSubtotal();

        if (subtotalResult.IsFailure)
            return subtotalResult.Error;

        var subtotal = subtotalResult.Value;

        if (discount.Currency != subtotal.Currency)
            return MoneyErrors.CurrencyMismatch;

        if (discount.Amount < 0 ||
            discount.Amount > subtotal.Amount)
        {
            return CartErrors.InvalidDiscount;
        }

        AppliedCouponCode = couponCode.Trim();
        AppliedDiscount = discount;

        return Result.Success();
    }

    public Result RemoveCoupon()
    {
        ClearAppliedDiscount();

        return Result.Success();
    }

    public Result<Money> GetSubtotal()
    {
        if (_items.Count == 0)
            return Money.Create(0, CurrencyConstant.UAH);

        var currency = _items[0].Currency;

        var totalAmount = _items.Sum(
            item => item.TotalPrice.Amount);

        return Money.Create(totalAmount, currency);
    }

    public Result<Money> GetDiscountTotal()
    {
        var subtotalResult = GetSubtotal();

        if (subtotalResult.IsFailure)
            return subtotalResult.Error;

        var subtotal = subtotalResult.Value;

        if (AppliedDiscount is null)
            return Money.Create(0, subtotal.Currency);

        if (AppliedDiscount.Currency != subtotal.Currency)
            return MoneyErrors.CurrencyMismatch;

        if (AppliedDiscount.Amount < 0 ||
            AppliedDiscount.Amount > subtotal.Amount)
        {
            return CartErrors.InvalidDiscount;
        }

        return AppliedDiscount;
    }

    public Result<Money> GetTotalCost()
    {
        var subtotalResult = GetSubtotal();

        if (subtotalResult.IsFailure)
            return subtotalResult.Error;

        var discountResult = GetDiscountTotal();

        if (discountResult.IsFailure)
            return discountResult.Error;

        var subtotal = subtotalResult.Value;
        var discount = discountResult.Value;

        var finalAmount = Math.Max(
            0,
            subtotal.Amount - discount.Amount);

        return Money.Create(finalAmount, subtotal.Currency);
    }

    private void ClearAppliedDiscount()
    {
        AppliedCouponCode = null;
        AppliedDiscount = null;
    }
}