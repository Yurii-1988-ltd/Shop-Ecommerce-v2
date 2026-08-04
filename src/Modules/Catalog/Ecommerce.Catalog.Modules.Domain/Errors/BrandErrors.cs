
using Ecommerce.Domain.Domain;

namespace Ecommerce.Catalog.Modules.Domain.Errors;

public static  class BrandErrors
{
    public static Error NameIsRequired
       => new("Name.IsRequired", "Name is required", ErrorType.Validation);
    public static Error NotFound(Guid id) =>
      Error.NotFound(
          "Brand.NotFound",
          $"Brand '{id}' was not found.");
}

