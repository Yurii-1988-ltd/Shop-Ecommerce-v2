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
}
