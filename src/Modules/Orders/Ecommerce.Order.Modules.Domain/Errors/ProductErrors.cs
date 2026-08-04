

namespace Ecommerce.Order.Modules.Domain.Errors;

public static class ProductErrors
{
    public static Error NotFound(Guid Id)
        => new Error("Not.Found", "Product is not found", ErrorType.NotFound);
}
