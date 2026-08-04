
using Ecommerce.Domain.Domain;
using Ecommerce.Mongo;
using Ecommerce.Order.Modules.Domain.Errors;
using Ecommerce.Order.Modules.Domain.Repositories;
using MongoDB.Driver;
using System.Xml.Linq;

namespace Ecommerce.Order.Modules.Infrastructure.Repositories;

internal sealed class OrderRepository : IOrderRepository
{
    private readonly IMongoCollection<Domain.Entities.Order> _collection;
    public OrderRepository(IMongoContext context)
    {
        _collection = context.GetCollection<Domain.Entities.Order>("orders");
        
    }
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => _collection.DeleteOneAsync(x => x.Id == id, cancellationToken);

    public async Task<bool> ExistOrderNumberAsync(
     string orderNumber,
     CancellationToken cancellationToken = default)
    {
        return await _collection
            .Find(x => x.OrderNumber == orderNumber)
            .AnyAsync(cancellationToken);
    }

    public async Task<Domain.Entities.Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
       
        var order = await _collection.Find(x => x.Id == orderId)
           .FirstOrDefaultAsync(cancellationToken);
       
        return order;

    }

    public async Task<(List<Domain.Entities.Order> Items, int TotalCount)> GetPagedAsync(
     int page,
     int pageSize,
     CancellationToken cancellationToken = default)
    {
        var filter = Builders<Domain.Entities.Order>.Filter.Empty;

        var totalCount = (int)await _collection.CountDocumentsAsync(
            filter,
            cancellationToken: cancellationToken);

        var items = await _collection
            .Find(filter)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }


    public Task InsertAsync(Domain.Entities.Order order, CancellationToken cancellationToken = default)

    {
        ArgumentNullException.ThrowIfNull(order);

        return _collection.InsertOneAsync(order, cancellationToken);


    }

    public async Task<Result> UpdateAsync(
    Domain.Entities.Order order,
    CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(order);

        var result = await _collection.ReplaceOneAsync(
            x => x.Id == order.Id,
            order,
            cancellationToken: cancellationToken);

        if (result.MatchedCount == 0)
           return OrderErrors.NotFound(order.Id);
        return Result.Success();
       
    }
}