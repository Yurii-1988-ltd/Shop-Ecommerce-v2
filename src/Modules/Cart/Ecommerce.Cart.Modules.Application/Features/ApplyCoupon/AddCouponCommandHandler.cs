using Ecommerce.Application.CQRS;
using Ecommerce.Cart.Modules.Domain.Errors;
using Ecommerce.Cart.Modules.Domain.Repositories;
using Ecommerce.Domain.Domain;

namespace Ecommerce.Cart.Modules.Application.Features.ApplyCoupon;

internal sealed class ApplyCouponCommandHandler(
    ICartRepository cartRepository,
    ICouponRepository couponRepository) : ICommandHandler<ApplyCouponCommand>
{
    public async Task<Result> Handle(ApplyCouponCommand request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);
        if (cart is null)
            return CartErrors.NotFound(request.CustomerId);

        // 2. Создаем Value Object купона из строки
        var codeResult = CouponCode.Create(request.Code);
        if (codeResult.IsFailure)
            return codeResult.Error;

      
        var coupon = await couponRepository.GetByCodeAsync(codeResult.Value, cancellationToken);
        if (coupon is null)
            return CouponErrors.NotFound(request.Code);

        
        var applyResult = cart.ApplyCoupon(coupon, DateTime.UtcNow);
        if (applyResult.IsFailure)
            return applyResult.Error;

       
        await cartRepository.UpdateAsync(cart, cancellationToken);

        return Result.Success();
    }
}