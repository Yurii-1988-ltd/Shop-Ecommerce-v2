

using Ecommerce.Domain.Domain;

namespace Ecommerce.Cart.Modules.Domain.Errors;

public sealed class CartErrors
{
    public static Error InvalidQuantity =>
        new Error("Invalid.Quantity", "Quantity is invalid", ErrorType.Validation);
    public static Error CartIsEmpty =>
        new Error("Cart.Empty", "Cart is empty", ErrorType.Validation);
    public static Error ProductAlreadyExists
        => new Error("Product.Already.Exist", "Product already exist", ErrorType.Validation);
    public static Error InvalidCustomerId
        = new Error("Invalid.CustomerId", "Customer Id is invalid", ErrorType.Validation);
    public static Error AlreadyExists(Guid id)
        => new Error($"Already.Exists", "Customer with {id} already exist", ErrorType.Validation);
    public static Error NotFound(Guid id)
    =>new Error($"Cart.NotFound", "Cart with id {id}", ErrorType.Validation);
}
