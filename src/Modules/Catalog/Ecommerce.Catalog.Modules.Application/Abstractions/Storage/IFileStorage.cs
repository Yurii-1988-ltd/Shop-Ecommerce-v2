

namespace Ecommerce.Catalog.Modules.Application.Abstractions.Storage;

public interface IFileStorage
{
    Task<string>UploadAsync(Stream stream,string fileName,CancellationToken cancellationToken = default);
    Task DeleteAsync(string storageKey,CancellationToken cancellationToken = default);

}
