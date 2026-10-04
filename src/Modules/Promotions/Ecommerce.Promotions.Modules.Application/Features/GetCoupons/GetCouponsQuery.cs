using Ecommerce.Application.Pagination;
using Ecommerce.Promotions.Modules.Application.Features.GetCoupon;

namespace Ecommerce.Promotions.Modules.Application.Features.GetCoupons;

public sealed record GetCouponsQuery(int Page = 1, int PageSize=20) : IQuery<PagedResult<CouponResponse>>;
