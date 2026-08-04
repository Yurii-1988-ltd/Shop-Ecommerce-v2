

namespace Ecommerce.Catalog.Modules.Infrastructure.Abstractions.Storage;

public sealed class LocalStorageOptions
{
    public const string SectionName = "LocalStorage";
    public string UploadPath { get; set; }
}
