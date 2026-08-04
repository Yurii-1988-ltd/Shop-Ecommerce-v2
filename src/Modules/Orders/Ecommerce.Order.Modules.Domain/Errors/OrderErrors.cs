

using Ecommerce.Domain.Domain;

namespace Ecommerce.Order.Modules.Domain.Errors;

public static class OrderErrors
{
    public static Error GuidEmpty
        => new("Empty.Guid", "Guid is Empty", ErrorType.Validation);
    public static Error InvalidPrice=>
        new ("Negative.Price", "Price must be greater than zero", ErrorType.Validation);
    public static Error NegativeQuantity=>
    new ("Negative.Quantity", "Quantity must be greater than zero", ErrorType.Validation);
    public static Error SkuIsRequired =>
        new("Sku.Required", "Sku is required", ErrorType.Validation);
    public static Error InvalidProductId=>
        new("Invalid.ProductId", "Product ID is invalid", ErrorType.Validation);    
    public static Error NameIsRequired=>
        new("Name.Required", "Name is required", ErrorType.Validation);
    public static Error NotFound(Guid id)
        => new Error("Order.NotFound", $"$Order with {id} not found", ErrorType.NotFound);
    public static Error AlreadyExist(Guid id)
        => new Error("Already.Exist", "Order already exist", ErrorType.Validation);
    public static Error OrderIsNotEditable =>
        new Error("Order.IsNot.Editable", "Order is not editable", ErrorType.Validation);
    public static Error InvalidStatusTransition(OrderStatus curreent, OrderStatus next)
        => new Error("Invalid.Status.Transition", $"Cannot change status from {curreent} to {next}", ErrorType.Conflict);
    public static Error OrderAlreadyDelivered() =>
        new Error("Order.Already.Delivered", "Order is already delivered", ErrorType.Conflict);
    public static Error OrderAlreadyCanceled()
        => new Error("Order.Already.Canceled", "Order is already canceled", ErrorType.Conflict);
    public static Error EmptyOrder()
        => new Error("Empty.Order", "Order can not be empty", ErrorType.Validation);
    public static Error ShippimhAddressRequired()
        => new Error("ShippingAddress.Required", "Shipping address is required", ErrorType.Validation);
    public static Error OrderCannotModdified(OrderStatus status)
        => new Error("Order.Cannot.Modified", $"Order with status {status} can not modified", ErrorType.Failure);
    public static Error OrderItemNotFound(Guid id)
        => new Error("Order.NotFound", $"Order with id {id} not found", ErrorType.NotFound);
    public static Error OrderNumberIsRequired(string orderNumber)
        => new("Order.Number.IsRequired", $"Order Number {orderNumber} is required", ErrorType.Validation);


}
