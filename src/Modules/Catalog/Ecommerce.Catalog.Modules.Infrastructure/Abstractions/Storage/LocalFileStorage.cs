using Microsoft.AspNetCore.Hosting;

namespace Ecommerce.Catalog.Modules.Infrastructure.Abstractions.Storage;

internal sealed class LocalFileStorage(IOptions<LocalStorageOptions>options, IWebHostEnvironment environment) : IFileStorage
{
    private readonly string _uploadDirectory = Path.Combine(
        environment.WebRootPath ?? environment.ContentRootPath,
        options.Value.UploadPath.Replace("wwwroot/", ""));
    public Task DeleteAsync(string storageKey, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(storageKey))
            return Task.CompletedTask;
        var fullPath = Path.Combine(_uploadDirectory, storageKey);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
        return Task.CompletedTask;
    }

    public async Task<string> UploadAsync(Stream stream, string fileName, CancellationToken cancellationToken = default)
    {
        if (!Directory.Exists(_uploadDirectory))
            Directory.CreateDirectory(_uploadDirectory);

        // СБРОС ПОЗИЦИИ ПОТОКА
        if (stream.CanSeek)
        {
            stream.Position = 0;
        }

        var extension = Path.GetExtension(fileName);
        var uniqueFileName = $"{Guid.NewGuid()}{extension}";
        var fullPath = Path.Combine(_uploadDirectory, uniqueFileName);

        using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write,
                            FileShare.None, 4096, useAsync: true))
        {
            await stream.CopyToAsync(fileStream, cancellationToken);
        }

        return uniqueFileName;
    }
}
