using Ecommerce.Storefront.ApiClients.Identity.Models;

namespace Ecommerce.Storefront.ApiClients.Identity.Authentication;

public interface IStorefrontAuthenticationService
{
    Task SignInAsync(AuthenticationResponse authenticationResponse, CancellationToken cancellationToken = default);
}
