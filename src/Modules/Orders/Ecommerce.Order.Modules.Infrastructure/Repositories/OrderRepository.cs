
using Ecommerce.Domain.Domain;
using Ecommerce.Order.Modules.Domain.Enums;
using Ecommerce.Order.Modules.Domain.Errors;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;

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
     string? search,
     OrderStatus? status,
     Guid? customerId,
     DateTime? from,
     DateTime? to,
     OrderSortBy sortBy,
     bool descending,
     CancellationToken cancellationToken = default)
    {
        var builder = Builders<Domain.Entities.Order>.Filter;
        var filter = builder.Empty;

        if (status is not null)
        {
            filter &= builder.Eq(x => x.Status, status.Value);
        }

        if (customerId is not null)
        {
            filter &= builder.Eq(x => x.CustomerId, customerId.Value);
        }

        if (from is not null)
        {
            filter &= builder.Gte(x => x.CreatedAtUtc, from.Value);
        }

        if (to is not null)
        {
            filter &= builder.Lte(x => x.CreatedAtUtc, to.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            filter &= builder.Regex(x => x.OrderNumber, new BsonRegularExpression(Regex.Escape(search), "i"));
        }

        var sort = sortBy switch
        {
            OrderSortBy.OrderNumber => descending
                ? Builders<Domain.Entities.Order>.Sort.Descending(x => x.OrderNumber)
                : Builders<Domain.Entities.Order>.Sort.Ascending(x => x.OrderNumber),

            OrderSortBy.Status => descending
                ? Builders<Domain.Entities.Order>.Sort.Descending(x => x.Status)
                : Builders<Domain.Entities.Order>.Sort.Ascending(x => x.Status),

            OrderSortBy.TotalQuantity => descending
                ? Builders<Domain.Entities.Order>.Sort.Descending(x => x.TotalQuantity)
                : Builders<Domain.Entities.Order>.Sort.Ascending(x => x.TotalQuantity),

            _ => descending
                ? Builders<Domain.Entities.Order>.Sort.Descending(x => x.CreatedAtUtc)
                : Builders<Domain.Entities.Order>.Sort.Ascending(x => x.CreatedAtUtc)
        };

        var totalCount = (int)await _collection.CountDocumentsAsync(
            filter,
            cancellationToken: cancellationToken);

        var items = await _collection
            .Find(filter)
            .Sort(sort)
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