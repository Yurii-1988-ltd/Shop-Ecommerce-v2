using Ecommerce.Catalog.Modules.Domain.Entities;
using Ecommerce.Mongo;

namespace Ecommerce.Catalog.Modules.Infrastructure.Repositories;

internal sealed class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _collection;

    public ProductRepository(IMongoContext context)
    {
        _collection = context.Database.GetCollection<Product>("Products");
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {

        return await _collection
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }


    public async Task InsertAsync(
        Product product,
        CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(product, cancellationToken: cancellationToken);
    }

    public Task ReplaceAsync(Product product, CancellationToken cancellationToken = default)
    {
        return _collection.ReplaceOneAsync(
            x => x.Id == product.Id,
            product,
            cancellationToken: cancellationToken);
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return _collection.DeleteOneAsync(
            x => x.Id == id, cancellationToken);
    }
    public async Task<bool> ExistOrderNumberAsync(string orderNumber, CancellationToken cancellationToken = default)
    {
        var model = new CreateIndexModel<Domain.Entities.Product>(
      Builders<Domain.Entities.Product>.IndexKeys.Ascending(x => x.ProductNumber),
      new CreateIndexOptions
      {
          Unique = true
      });

        await _collection.Indexes.CreateOneAsync(model);
        return true;
    }
}
