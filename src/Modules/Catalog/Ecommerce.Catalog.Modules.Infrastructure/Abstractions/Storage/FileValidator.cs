

using Ecommerce.Catalog.Modules.Domain.Errors;
using Ecommerce.Domain.Domain;

namespace Ecommerce.Catalog.Modules.Infrastructure.Abstractions.Storage;

internal sealed class FileValidator : IFileValidator
{
    private readonly FileStorageOptions _options;
    public FileValidator(IOptions<FileStorageOptions>options)
    {
        _options = options.Value;

        
    }
    public Result Validate(string fileName, string contentType, long length)
    {
      if(length<=0)
            return Result.Failure (FileErrors.Empty);
      if(length > _options.MaxFileSizeInBytes)
            return Result.Failure (FileErrors.TooLarge);
      var extension = Path.GetExtension (fileName);
        if (string.IsNullOrEmpty(extension) ||
                !_options.AllowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
            return Result.Failure(FileErrors.InvalidExtensions);
        if (string.IsNullOrWhiteSpace(contentType) ||
      !_options.AllowedContentTypes.Contains(contentType, StringComparer.OrdinalIgnoreCase))
        {
            return FileErrors.InvalidContentType;
        }
        return Result.Success();
                



    }
}
