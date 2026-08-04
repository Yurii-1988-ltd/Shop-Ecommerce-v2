

using Ecommerce.Domain.Domain;

namespace Ecommerce.Catalog.Modules.Domain.Errors;

public static class CategoryErrors
{
    public static Error NameIsRequired
        => new("Name.IsRequired", "Name is required", ErrorType.Validation);
    public static Error NotFound(Guid id) =>
        Error.NotFound(
            "Category.NotFound",
            $"Category '{id}' was not found.");
}
