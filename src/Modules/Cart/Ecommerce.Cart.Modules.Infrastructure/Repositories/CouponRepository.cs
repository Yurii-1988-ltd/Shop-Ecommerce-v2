

using Ecommerce.Cart.Modules.Domain.Entities;
using Ecommerce.Cart.Modules.Domain.ValueObjects;
using MongoDB.Driver;

namespace Ecommerce.Cart.Modules.Infrastructure.Repositories;

internal sealed class CouponRepository : ICouponRepository
{
    private readonly IMongoCollection<Coupon> _collection;

    public CouponRepository(IMongoContext context)
    {
        _collection = context.GetCollection<Coupon>("coupons");
    }

    public  Task AddAsync(Coupon coupon, CancellationToken cancellationToken)
    {
        return  _collection.InsertOneAsync(coupon, cancellationToken);
    }

    public async Task<Coupon?> GetByCodeAsync(CouponCode code, CancellationToken cancellationToken)
    {
        return await _collection.Find(x => x.Code == code)
             .FirstOrDefaultAsync(cancellationToken);
    }
}
