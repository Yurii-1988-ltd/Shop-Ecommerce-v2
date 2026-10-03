

using Ecommerce.Domain.Domain;

namespace Ecommerce.Promotions.Contracts;

public interface ICouponService
{
    Task<Result<DiscountResultDto>> ValidateAsync(string code,
        decimal subtotal,
        string currency,
        CancellationToken cancellationToken = default);
}
