

using Ecommerce.Cart.Modules.Domain.Entities;
using Ecommerce.Cart.Modules.Domain.ValueObjects;

namespace Ecommerce.Cart.Modules.Domain.Repositories
{
    public  interface ICouponRepository
    {
        Task<Coupon?> GetByCodeAsync(CouponCode code, CancellationToken cancellationToken);
        Task AddAsync(Coupon coupon,CancellationToken cancellationToken);
    }
}
