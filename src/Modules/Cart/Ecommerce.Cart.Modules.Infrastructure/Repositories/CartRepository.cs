using MongoDB.Driver;

namespace Ecommerce.Cart.Modules.Infrastructure.Repositories;

internal sealed class CartRepository : ICartRepository
{
    private readonly IMongoCollection<Domain.Entities.Cart> _collection;

    public CartRepository(IMongoContext context)
    {
        _collection = context.GetCollection<Domain.Entities.Cart>("carts");
    }
    public async Task<Domain.Entities.Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
        
    }

    public async Task<Domain.Entities.Cart?> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        var cart =  await _collection.Find(x => x.CustomerId == customerId)
            .FirstOrDefaultAsync(cancellationToken);
        return cart;
    }

    public Task InsertAsync(Domain.Entities.Cart cart, CancellationToken cancellationToken = default)
    {
        return _collection.InsertOneAsync(cart, cancellationToken: cancellationToken);
    }

    public Task ReplaceAsync(Domain.Entities.Cart cart, CancellationToken cancellationToken = default)
    {
       return _collection.ReplaceOneAsync(x => x.Id == cart.Id, cart, cancellationToken: cancellationToken);
    }

    public  Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
       return  _collection.DeleteOneAsync(x=>x.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(Domain.Entities.Cart cart, CancellationToken cancellationToken = default)
    {
        var result = await _collection.ReplaceOneAsync(x => x.Id == cart.Id, cart, cancellationToken: cancellationToken);
        if (result.MatchedCount == 0)
        {
            throw new InvalidOperationException($"Cart with id {cart.Id} not found");
        }
    }
    public async Task<(List<Domain.Entities.Cart> Items, int TotalCount)> GetPagedAsync(
    int page,
    int pageSize,
    CancellationToken cancellationToken = default)
    {
        var filter = Builders<Domain.Entities.Cart>.Filter.Empty;

        var totalCount = (int)await _collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);

        var items = await _collection
            .Find(filter)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

}
