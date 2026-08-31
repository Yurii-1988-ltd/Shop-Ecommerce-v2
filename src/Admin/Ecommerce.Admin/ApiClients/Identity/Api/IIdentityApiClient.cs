using Ecommerce.Admin.ApiClients.Identity.Contracts;
using Ecommerce.Admin.ApiClients.Identity.Responses;

namespace Ecommerce.Admin.ApiClients.Identity.Api;

public interface IIdentityApiClient
{
    Task<AuthenticationResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
}
