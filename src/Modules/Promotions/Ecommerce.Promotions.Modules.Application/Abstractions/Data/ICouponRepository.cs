

namespace Ecommerce.Promotions.Modules.Application.Abstractions.Data;

public interface ICouponRepository
{
    Task<int> CountAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<Coupon>> ListCouponsAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task<Coupon?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Coupon?> GetByCodeAsync(CouponCode code, CancellationToken cancellationToken);
    Task AddAsync(Coupon coupon, CancellationToken cancellationToken);
    Task UpdateAsync(Coupon coupon, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);

}
