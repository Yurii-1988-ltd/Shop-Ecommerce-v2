

using Ecommerce.Cart.Modules.Domain.Errors;
using Ecommerce.Domain.Constants;
using Ecommerce.Domain.Domain;
using Ecommerce.Domain.Errors;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Cart.Modules.Domain.Entities;

public sealed class Cart : Entity
{
    private readonly List<CartItem> _items = new();

    public Guid CustomerId { get; private set; }
    public List<CartItem> Items = [];
    private Cart() { }

    public Cart(Guid id,Guid customerId)
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


    public Result AddItem(Guid productId, string name , Money price,int quantity)
    {
        if(Items.Any()&& Items[0].Currency!=price.Currency)
        {
            return MoneyErrors.CurrencyMismatch;
        }
        var existItems = Items.SingleOrDefault(x => x.ProductId==productId);
        if (existItems != null)
            return existItems.UpdateQuantity(existItems.Quantity + quantity);
        var itemsResult = CartItem.Create(productId, name, price, quantity);
        if(itemsResult.IsFailure)
            return Result.Failure(itemsResult.Error);
        Items.Add(itemsResult.Value);
        return Result.Success();


    }
    public Result RemoveItem(Guid productId)
    {
        var items = Items.SingleOrDefault(y => y.ProductId==productId);
        if(items == null)
        {
            return CartItemErrors.NotFound(productId);
        }
        Items.Remove(items);
        return Result.Success();

    }
    public Result<Money> GetTotalCost()
    {
        if(!Items.Any())
        {
            return Money.Create(0, CurrencyConstant.UAH);
        }
        Money total = Items[0].TotalPrice;
        for (int i = 1; i < Items.Count; i++)
        {
            var addResult = total.Add(Items[i].TotalPrice);
            if (addResult.IsFailure)
            {
                return Result.Failure<Money>(addResult.Error);
            }
            total = addResult.Value;
        }
        return total;
    }
    public Result ChangeQuantity(Guid productId, int quantity)
    {
        var item = Items.SingleOrDefault(x => x.ProductId == productId);

        if (item is null)
            return CartItemErrors.NotFound(productId);

        return item.UpdateQuantity(quantity);
    }
    public void Clear()
    {
        Items.Clear();
    }
}

