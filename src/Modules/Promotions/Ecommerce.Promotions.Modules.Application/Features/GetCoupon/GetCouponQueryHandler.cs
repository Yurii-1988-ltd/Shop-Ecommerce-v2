

namespace Ecommerce.Promotions.Modules.Application.Features.GetCoupon;

internal sealed class GetCouponQueryHandler(ICouponRepository repository) : IQueryHandler<GetCouponQuery, CouponResponse>
{
    public async Task<Result<CouponResponse>> Handle(GetCouponQuery request, CancellationToken cancellationToken)
    {
        var coupon = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (coupon is null)
            return CouponErrors.NotFound(request.Id);
        return new CouponResponse(coupon.Id, coupon.Code.Value);
    }
}
