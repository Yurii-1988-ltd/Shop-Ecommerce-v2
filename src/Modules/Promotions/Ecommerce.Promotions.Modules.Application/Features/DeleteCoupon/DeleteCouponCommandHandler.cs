
namespace Ecommerce.Promotions.Modules.Application.Features.DeleteCoupon;

internal sealed class DeleteCouponCommandHandler(ICouponRepository repository) : ICommandHandler<DeleteCouponCommand>
{
    public async Task<Result> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
    {
        var coupon = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (coupon is null)
            return CouponErrors.NotFound(request.Id);
        await repository.DeleteAsync(request.Id, cancellationToken);
        return Result.Success();
    }
}

