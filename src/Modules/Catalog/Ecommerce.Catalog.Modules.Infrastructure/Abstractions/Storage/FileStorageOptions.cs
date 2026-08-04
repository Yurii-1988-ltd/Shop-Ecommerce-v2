

namespace Ecommerce.Catalog.Modules.Infrastructure.Abstractions.Storage;

internal sealed class FileStorageOptions
{
    public const string SectionName = "FileStorage";

    public long MaxFileSizeInBytes { get; init; }

    public string[] AllowedExtensions { get; init; } = Array.Empty<string>();

    public string[] AllowedContentTypes { get; init; } = Array.Empty<string>();
}
