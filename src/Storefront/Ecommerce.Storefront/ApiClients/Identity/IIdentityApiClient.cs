using Ecommerce.Storefront.ApiClients.Identity.Contracts;
using Ecommerce.Storefront.ApiClients.Identity.Models;

namespace Ecommerce.Storefront.ApiClients.Identity;

public interface IIdentityApiClient
{
    Task<AuthenticationResponse?> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default);
    Task<AuthenticationResponse?> RefreshTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken = default);
    Task<bool> UpdateProfileAsync(
        UpdateProfileRequest request,
        CancellationToken cancellationToken = default);
    Task<UserProfileResponse?> GetProfileAsync(
        CancellationToken cancellationToken = default);
    Task<AuthenticationResponse?> RegisterAsync(
        RegisterRequest request,
        CancellationToken cancellationToken = default);
}
