using Ecommerce.Basket.Modules.Domain.Enums;
using Ecommerce.Basket.Modules.Domain.Errors;
using Ecommerce.Domain.Domain;
using Ecommerce.Domain.ValueObjects;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Basket.Modules.Domain.Entities;

public sealed class Basket : Entity
{
    #region Properties and Constructors
    public const int MaxDistinctItems = 50;
    private List<BasketItem> _items = [];
    public Guid CustomerId { get; private set; }
    public BasketStatus Status { get; private set; }
    public Coupon? AppliedCoupon { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? LastModifiedUtc { get; private set; }
    public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();


    public string Currency => _items.FirstOrDefault()?.UnitPrice.Currency ?? "UAH";

    private Basket()
    {
    }

    public Basket(Guid customerId)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        Status = BasketStatus.Active;
        CreatedAtUtc = DateTime.UtcNow;
    }
    #endregion

    #region Static Factory Methods
    public static Result<Basket> Create(Guid customerId)
    {
        if (customerId == Guid.Empty)
            return Result.Failure<Basket>(BasketErrors.EmptyCustomerId);

        return Result.Success(new Basket(customerId));
    }
    #endregion

    #region Domain Actions
    public Result AddOrUpdateItem(Guid productId, string productName, Money unitPrice, int quantity)
    {
        var activeCheck = EnsureActive();
        if (activeCheck.IsFailure) return activeCheck;

        if (quantity <= 0)
            return Result.Failure(BasketErrors.InvalidQuantity);

        var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);

        if (existingItem != null)
        {
            var updateResult = existingItem.UpdateQuantity(existingItem.Quantity.Value + quantity);
            if (updateResult.IsFailure) return updateResult;
        }
        else
        {
            if (_items.Count >= MaxDistinctItems)
                return Result.Failure(BasketErrors.MaxDistinctItemsReached);

            var itemResult = BasketItem.Create(productId, productName, unitPrice, quantity);
            if (itemResult.IsFailure) return itemResult.Error;

            _items.Add(itemResult.Value);
        }

        Touch();
        return Result.Success();
    }

    public Result ChangeItemQuantity(Guid productId, int newQuantity)
    {
        var activeCheck = EnsureActive();
        if (activeCheck.IsFailure) return activeCheck;

        if (newQuantity == 0)
            return RemoveItem(productId);

        if (newQuantity < 0)
            return Result.Failure(BasketErrors.InvalidQuantity);

        var item = _items.FirstOrDefault(x => x.ProductId == productId);
        if (item is null)
            return Result.Failure(BasketErrors.ItemNotFound);

        var updateResult = item.UpdateQuantity(newQuantity);
        if (updateResult.IsFailure) return updateResult;

        Touch();
        return Result.Success();
    }

    private Result RemoveItem(Guid productId)
    {
        var activeCheck = EnsureActive();
        if (activeCheck.IsFailure) return activeCheck;

        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item is null)
            return Result.Failure(BasketErrors.ItemNotFound);

        _items.Remove(item);

        if (AppliedCoupon != null && !AppliedCoupon.IsSatisfiedBy(RawSubtotal))
        {
            ClearCoupon();
        }

        Touch();
        return Result.Success();
    }

    public Result ApplyCoupon(Coupon coupon, DateTime currentDateUtc)
    {
        var activeCheck = EnsureActive();
        if (activeCheck.IsFailure) return activeCheck;

        if (!coupon.IsValid(currentDateUtc))
            return Result.Failure(BasketErrors.CouponExpired);

        if (!coupon.IsSatisfiedBy(RawSubtotal))
            return Result.Failure(BasketErrors.CouponMinimumSpendNotMet);

        AppliedCoupon = coupon;
        Touch();
        return Result.Success();
    }

    public Result RemoveCoupon()
    {
        var activeCheck = EnsureActive();
        if (activeCheck.IsFailure) return activeCheck;

        AppliedCoupon = null;
        Touch();
        return Result.Success();
    }

    public Result StartCheckout()
    {
        var activeCheck = EnsureActive();
        if (activeCheck.IsFailure) return activeCheck;

        if (!_items.Any())
            return Result.Failure(BasketErrors.EmptyBasketCheckout);

        Status = BasketStatus.CheckoutStarted;
        Touch();
        return Result.Success();
    }

    public void ClearCoupon()
    {
        AppliedCoupon = null;
    }
    #endregion

    #region Helper Methods
    private Result EnsureActive()
    {
        return Status == BasketStatus.Active
            ? Result.Success()
            : Result.Failure(BasketErrors.BasketNotActive);
    }

    public void Touch()
        => LastModifiedUtc = DateTime.UtcNow;

    // --- Calculated Domain Properties ---

    public Money RawSubtotal => _items.Count == 0
        ? Money.Zero(Currency)
        : new Money(_items.Sum(i => i.LineTotal.Amount), Currency);

    public Money DiscountTotal => AppliedCoupon?.CalculateDiscount(RawSubtotal)
        ?? Money.Zero(Currency);

    public Money GrandTotal => new(
        Math.Max(0, RawSubtotal.Amount - DiscountTotal.Amount),
        Currency);

    public Money CalculateTotalPrice() => GrandTotal;
    #endregion
}