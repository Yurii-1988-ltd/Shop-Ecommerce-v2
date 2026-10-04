using Ecommerce.Application.Pagination;
using Ecommerce.Promotions.Modules.Application.Abstractions.Data;
using Ecommerce.Promotions.Modules.Application.Features.GetCoupon;

namespace Ecommerce.Promotions.Modules.Application.Features.GetCoupons;

internal sealed class GetCouponsQueryHandler(
    ICouponRepository repository)
    : IQueryHandler<GetCouponsQuery, PagedResult<CouponResponse>>
{
    public async Task<Result<PagedResult<CouponResponse>>> Handle(
        GetCouponsQuery request,
        CancellationToken cancellationToken)
    {
        var page = Math.Max(request.Page, 1);
        var pageSize = Math.Clamp(request.PageSize, 1, 100);

        var coupons = await repository.ListCouponsAsync(
            page,
            pageSize,
            cancellationToken);

        var totalCount = await repository.CountAsync(
            cancellationToken);

        var items = coupons
            .Select(coupon => new CouponResponse(
                coupon.Id,
                coupon.Code.Value))
            .ToList();

        return new PagedResult<CouponResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
}