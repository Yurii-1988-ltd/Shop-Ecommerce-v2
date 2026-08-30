using Ecommerce.Admin.ApiClients.Coupons.Contracts;
using Ecommerce.Admin.ApiClients.Coupons.Responses;
using System.Net;

internal sealed class CouponApiClient(HttpClient httpClient)
    : ICouponApiClient
{
    private const string CouponsUrl = "/admin/coupons";

    public async Task<Guid> CreateAsync(
        CreateCouponRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync(
            CouponsUrl,
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        var result = await response.Content
            .ReadFromJsonAsync<CouponResponse>(cancellationToken);

        return result!.Id;
    }

    public async Task<CouponResponse?> GetAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync(
            $"{CouponsUrl}/{id}",
            cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadFromJsonAsync<CouponResponse>(cancellationToken);
    }

    public async Task<PagedResult<CouponResponse>> GetCouponsAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync(
            $"{CouponsUrl}?page={page}&pageSize={pageSize}",
            cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadFromJsonAsync<PagedResult<CouponResponse>>(cancellationToken)
            ?? throw new InvalidOperationException("Empty response.");
    }

    public async Task UpdateAsync(
        Guid id,
        UpdateCouponRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync(
            $"{CouponsUrl}/{id}",
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync(
            $"{CouponsUrl}/{id}",
            cancellationToken);

        response.EnsureSuccessStatusCode();
    }
}