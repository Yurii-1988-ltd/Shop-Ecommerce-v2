

using Ecommerce.Domain.Domain;

namespace Ecommerce.Catalog.Modules.Domain.Errors;

public static class ImagesErrors
{
    public static Error UrlIsRequired
        => new Error("Url.Empty", "Url can not be empty", ErrorType.Validation);
    public static Error InvalidProductId
        => new Error("Invalid.ProductId", "Product Id is invalid", ErrorType.Validation);
    public static Error InvalidDisplayOrder
       => new Error("Invalid.DisplayOrder", "Displsay order is invalid", ErrorType.Validation);

    public static Error NotFound(Guid id) =>
        new Error("Not.Found",$"Image '{id}'not found", ErrorType.NotFound);



    

}
