using Ecommerce.Cart.Modules.Domain.Errors;
using Ecommerce.Domain.Constants;
using Ecommerce.Domain.Domain;
using Ecommerce.Domain.Errors;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Cart.Modules.Domain.Entities;

public sealed class Cart : Entity
{
    #region private fields and constructors

    private List<CartItem> _items = new();

    public Guid CustomerId { get; private set; }
    public Coupon? AppliedCoupon { get; set; }

    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

    private Cart() { }

    public Cart(Guid id, Guid customerId)
    {
        Id = id;
        CustomerId = customerId;
    }
    #endregion
    #region static factory Methods

    public static Result<Cart> Create(Guid customerId)
    {
        if (customerId == Guid.Empty)
            return CartErrors.InvalidCustomerId;

        return new Cart(Guid.NewGuid(), customerId);
    }

    public Result AddItem(Guid productId, string name, Money price, int quantity)
    {
        // 2. Используем _items вместо Items
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

        // 3. Добавляем в приватное поле _items
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
        return Result.Success();
    }

    public Result ChangeQuantity(Guid productId, int quantity)
    {
        var item = _items.SingleOrDefault(x => x.ProductId == productId);
        if (item is null)
            return CartItemErrors.NotFound(productId);

        return item.UpdateQuantity(quantity);
    }

    public void Clear()
    {
        _items.Clear();
        AppliedCoupon=null;
    }

    public Result<Money> GetTotalCost()
    {
        if (!_items.Any())
        {
            return Money.Create(0, CurrencyConstant.UAH);
        }

        // 4. Более чистый и безопасный подсчет через LINQ Sum
        var currency = _items[0].Currency;
        var totalAmount = _items.Sum(x => x.TotalPrice.Amount);

        return Money.Create(totalAmount, currency);
    }
    #endregion
}