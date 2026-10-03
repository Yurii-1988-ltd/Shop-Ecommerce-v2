using Ecommerce.Cart.Modules.Application.Features.ApplyCoupon;
using Ecommerce.Promotions.Contracts;

namespace Ecommerce.Cart.Modules.Application.Features.Coupons.ApplyCoupon;

public sealed class ApplyCouponCommandHandler(
    ICartRepository cartRepository,
    ICouponService couponService) : ICommandHandler<ApplyCouponCommand>
{
    public async Task<Result> Handle(
        ApplyCouponCommand request,
        CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetByCustomerIdAsync(
            request.CustomerId,
            cancellationToken);

        if (cart is null)
            return CartErrors.NotFound(request.CustomerId);

        var subtotalResult = cart.GetSubtotal();

        if (subtotalResult.IsFailure)
            return subtotalResult.Error;

        var subtotal = subtotalResult.Value;

        var discountResult = await couponService.ValidateAsync(
            request.Code,
            subtotal.Amount,
            subtotal.Currency,
            cancellationToken);

        if (discountResult.IsFailure)
            return discountResult.Error;

        var applyResult = cart.ApplyCoupon(
            discountResult.Value.CouponCode,
            discountResult.Value.Discount);

        if (applyResult.IsFailure)
            return applyResult.Error;

        await cartRepository.UpdateAsync(
            cart,
            cancellationToken);

        return Result.Success();
    }
}