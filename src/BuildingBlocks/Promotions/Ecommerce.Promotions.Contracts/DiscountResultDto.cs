using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Promotions.Contracts;

public sealed record DiscountResultDto(
    Money Discount,
    string CouponCode);