

namespace Ecommerce.Promotions.Modules.Infrastructure.Repositories;

internal sealed class CouponRepository : ICouponRepository
{
    private readonly IMongoCollection<Coupon> _collection;

    public CouponRepository(IMongoContext context)
    {
        _collection = context.GetCollection<Coupon>("coupons");
    }

    public Task AddAsync(Coupon coupon, CancellationToken cancellationToken)
    {
        return _collection.InsertOneAsync(coupon, cancellationToken);
    }

    public async Task<int> CountAsync(
      CancellationToken cancellationToken)
    {
        var count = await _collection.CountDocumentsAsync(
            _ => true,
            cancellationToken: cancellationToken);

        return (int)count;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
         await _collection.DeleteOneAsync(x => x.Id == id, cancellationToken);

    }

    public async Task<Coupon?> GetByCodeAsync(CouponCode code, CancellationToken cancellationToken)
    {
        return await _collection.Find(x => x.Code == code)
             .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Coupon?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _collection.Find(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Coupon>> ListCouponsAsync(
     int page,
     int pageSize,
     CancellationToken cancellationToken)
    {
        return await _collection
            .Find(_ => true)
            .SortBy(x => x.Code)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Coupon coupon, CancellationToken cancellationToken)
    {
        var result = await _collection.ReplaceOneAsync(x => x.Id == coupon.Id,
            coupon,
            cancellationToken: cancellationToken);
        if(result.MatchedCount == 0)
            throw new InvalidOperationException($"Coupon with ID '{coupon.Id}' was not found");

    }
}
