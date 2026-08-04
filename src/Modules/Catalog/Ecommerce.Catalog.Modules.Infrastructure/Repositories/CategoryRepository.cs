

using Ecommerce.Catalog.Modules.Domain.Entities;
using Ecommerce.Mongo;

namespace Ecommerce.Catalog.Modules.Infrastructure.Repositories;

internal sealed class CategoryRepository : ICategoryRepository

{
    private readonly IMongoCollection<Category> _collection;

    public CategoryRepository(IMongoContext context)
    {
        _collection = context.Database.GetCollection<Category>("Categories");
    }
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return _collection.DeleteOneAsync(
               x => x.Id == id, cancellationToken);
    }

    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _collection
              .Find(x => x.Id == id)
              .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task InsertAsync(Category category, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(category, cancellationToken: cancellationToken);
    }

    public Task ReplaceAsync(Category category, CancellationToken cancellationToken)
    {
        return _collection.ReplaceOneAsync(
            x => x.Id == category.Id,
            category); 
    }

  
}


