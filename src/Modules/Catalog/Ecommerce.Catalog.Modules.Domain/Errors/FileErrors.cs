
using Ecommerce.Domain.Domain;

namespace Ecommerce.Catalog.Modules.Domain.Errors;

public static class FileErrors
{
    public static Error Empty =>
        new("Files.Empty", "File is empty", ErrorType.Validation);
    public static Error TooLarge
        => new("Files.ToLarge", "File size exceeds the limit.", ErrorType.Validation);
    public static Error InvalidExtensions
        =>  new("Files.InvalidExtension", "Unsupported file extension.", ErrorType.Validation);
    public static readonly Error InvalidContentType =
        new("Files.InvalidContentType", "Unsupported content type.",ErrorType.Validation);

}
