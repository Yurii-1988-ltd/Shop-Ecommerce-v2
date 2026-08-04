
namespace Ecommerce.Catalog.Modules.Application.Abstractions.Storage;

public interface IFileValidator
{
    Result Validate(string fileName, string contentType, long length);
}
