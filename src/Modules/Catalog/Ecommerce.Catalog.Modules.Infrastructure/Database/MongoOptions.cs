

namespace Ecommerce.Catalog.Modules.Infrastructure.Database;

public sealed class MongoOptions
{
    public const string SectionName = "MongoDb";
    public string ConnectionString { get; init; } = string.Empty;
    public string Database { get; init; } = string.Empty;
}
