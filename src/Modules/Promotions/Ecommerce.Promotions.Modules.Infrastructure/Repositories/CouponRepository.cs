

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

    public async Task UpdateAsync(Coupon coupon, CancellationToken cancellationToken)
    {
        var result = await _collection.ReplaceOneAsync(x => x.Id == coupon.Id,
            coupon,
            cancellationToken: cancellationToken);
        if(result.MatchedCount == 0)
            throw new InvalidOperationException($"Coupon with ID '{coupon.Id}' was not found");

    }
}
