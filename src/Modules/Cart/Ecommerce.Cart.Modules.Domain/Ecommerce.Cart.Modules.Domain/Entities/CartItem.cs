using Ecommerce.Cart.Modules.Domain.Errors;
using Ecommerce.Domain.Domain;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Cart.Modules.Domain.Entities;

public sealed class CartItem : Entity
{
    public Guid ProductId { get; private set; }
    public string Name { get; private set; }
    public string Sku { get;private set; }
    public Money Price { get; private set; }
    public int Quantity { get; private set; }

    // Берем валюту напрямую из объекта Money
    public string Currency => Price.Currency;
    public Money TotalPrice => Price.Multiply(Quantity).Value;

    private CartItem(){}

    private CartItem(Guid id, Guid productId, string name, string sku, Money price, int quantity)
    {
        Id = id; // Установка Id из базового Entity
        ProductId = productId;
        Name = name;
        Sku = sku;
        Price = price;
        Quantity = quantity;
    }

    public static Result<CartItem> Create(Guid productId, string name, string sku, Money price, int quantity)
    {
        if (productId == Guid.Empty)
            return CartItemErrors.InvalidProductId;
        if (string.IsNullOrWhiteSpace(name))
            return CartItemErrors.NameIsRequired;
        if (string.IsNullOrWhiteSpace(sku))
            return CartItemErrors.SkuIsRequired;
        if (quantity <= 0)
            return CartItemErrors.NegativeQuantity;
        if (price is null)
            return CartItemErrors.InvalidPrice;

        return new CartItem(Guid.NewGuid(), productId, name, sku, price, quantity);
    }

    public Result UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0)
            return CartItemErrors.NegativeQuantity;

        Quantity = newQuantity;
        return Result.Success();
    }
}