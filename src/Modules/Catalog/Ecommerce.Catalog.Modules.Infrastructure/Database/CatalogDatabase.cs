

using Ecommerce.Catalog.Modules.Domain.Entities;
using Ecommerce.Mongo;

namespace Ecommerce.Catalog.Modules.Infrastructure.Database;

public sealed class CatalogDatabase : ICatalogDatabase
{
    public IMongoCollection<Product> Products { get; }
    public IMongoCollection<Category> Categories { get; }
    public IMongoCollection<Brand> Brands { get; }

    public CatalogDatabase(IMongoContext context)
    {
        Products = context.GetCollection<Product>("Products");
        Categories = context.GetCollection<Category>("Categories");
        Brands = context.GetCollection<Brand>("Brands");
    }
}
