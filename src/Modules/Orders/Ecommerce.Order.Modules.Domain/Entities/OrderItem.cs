
using MongoDB.Bson.Serialization.Attributes;

namespace Ecommerce.Order.Modules.Domain.Entities;

public sealed class OrderItem : Entity
{
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public string SKU { get; private set; } = string.Empty;
    public Money UnitPrice { get; private set; } = default!;
    public int Quantity { get; private set; }

    public Money TotalPrice => UnitPrice.Multiply(Quantity).Value;

    private OrderItem()
    {
    }

 //   [BsonConstructor]
    private OrderItem(
        Guid id,
        Guid productId,
        string productName,
        string sku,
        Money unitPrice,
        int quantity)
    {
        Id = id;
        ProductId = productId;
        ProductName = productName;
        SKU = sku;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    public static Result<OrderItem> Create(
        Guid productId,
        string productName,
        string sku,
        Money unitPrice,
        int quantity)
    {
        if (productId == Guid.Empty)
            return OrderErrors.InvalidProductId;

        if (string.IsNullOrWhiteSpace(productName))
            return OrderErrors.NameIsRequired;

        if (string.IsNullOrWhiteSpace(sku))
            return OrderErrors.SkuIsRequired;

        if (unitPrice is null)
            return OrderErrors.InvalidPrice;

        if (quantity <= 0)
            return OrderErrors.NegativeQuantity;

        var orderItemResult =  new OrderItem(
            Guid.NewGuid(),
            productId,
            productName,
            sku,
            unitPrice,
            quantity);
        return orderItemResult;
        
    }

    internal Result AddQuantity(int quantity)
    {
        if (quantity <= 0)
            return OrderErrors.NegativeQuantity;

        Quantity += quantity;

        return Result.Success();
    }

    internal Result ChangeQuantity(int quantity)
    {
        if (quantity <= 0)
            return OrderErrors.NegativeQuantity;

        Quantity = quantity;

        return Result.Success();
    }

}