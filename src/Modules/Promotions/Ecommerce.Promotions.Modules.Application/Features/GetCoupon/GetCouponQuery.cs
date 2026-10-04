

namespace Ecommerce.Promotions.Modules.Application.Features.GetCoupon;

public sealed record GetCouponQuery(Guid Id) : IQuery<CouponResponse>;


