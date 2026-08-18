

namespace Ecommerce.Basket.Modules.Infrastructure.Repositories;

internal sealed class MongoBasketRepository : IBasketRepository
{
    private readonly IMongoCollection<Domain.Entities.Basket> _collection;
    public MongoBasketRepository(IMongoContext context)
    {
        _collection = context.GetCollection<Domain.Entities.Basket>("baskets");
        
    }
    public async Task AddAsync(Domain.Entities.Basket basket, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(basket,cancellationToken:cancellationToken);
        
    }

    public async Task<Result<Domain.Entities.Basket>> GetByCustomerIdAsync(
     Guid customerId,
     CancellationToken cancellationToken = default)
    {
        var basket = await _collection
            .Find(b => b.CustomerId == customerId && b.Status == BasketStatus.Active)
            .FirstOrDefaultAsync(cancellationToken);

        if (basket is null)
        {
            // Передаем customerId, так как basket равен null
            return Result.Failure<Domain.Entities.Basket>(BasketErrors.NotFoundByCustomer(customerId));
        }

        return Result.Success(basket);
    }

    public async Task<Result> DeleteAsync(
     Guid id,
     CancellationToken cancellationToken = default)
    {
        var result = await _collection.DeleteOneAsync(
            b => b.Id == id,
            cancellationToken);

        if (result.DeletedCount == 0)
        {
            return Result.Failure(
                BasketErrors.NotFound(id));
        }

        return Result.Success();
    }

    public async Task<Result> UpdateAsync(Domain.Entities.Basket basket, CancellationToken cancellationToken = default)
    {
        var result = await _collection.ReplaceOneAsync(
            b=>b.Id==basket.Id,
            basket,
            cancellationToken:cancellationToken);
        if(result.MatchedCount==0)
        {
            return Result.Failure(BasketErrors.NotFound(basket.Id));
        }
        return Result.Success();
    }
}
