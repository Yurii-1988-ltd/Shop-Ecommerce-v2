

namespace Ecommerce.Promotions.Modules.Application.Abstractions.Data;

public interface ICouponRepository
{
    Task<Coupon?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Coupon?> GetByCodeAsync(CouponCode code, CancellationToken cancellationToken);
    Task AddAsync(Coupon coupon, CancellationToken cancellationToken);
    Task UpdateAsync(Coupon coupon, CancellationToken cancellationToken);
}
