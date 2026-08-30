using Ecommerce.Admin.ApiClients.Coupons.Contracts;
using Ecommerce.Admin.ApiClients.Coupons.Responses;

public interface ICouponApiClient
{
    Task<Guid> CreateAsync(
        CreateCouponRequest request,
        CancellationToken cancellationToken = default);

    Task<CouponResponse?> GetAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<PagedResult<CouponResponse>> GetCouponsAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Guid id,
        UpdateCouponRequest request,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}