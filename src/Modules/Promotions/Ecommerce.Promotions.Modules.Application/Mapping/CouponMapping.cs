
using Ecommerce.Promotions.Modules.Application.Features.GetCoupon;

namespace Ecommerce.Promotions.Modules.Application.Mapping;

public static class CouponMapping
{
    public static CouponResponse ToResponse(this Coupon coupon)
    {
        return new CouponResponse(coupon.Id, coupon.Code.Value);
    }
}
