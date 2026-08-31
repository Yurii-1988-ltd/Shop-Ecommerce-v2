
using Ecommerce.Admin.ApiClients.Identity.Api;
using Ecommerce.Admin.ApiClients.Identity.Contracts;

namespace Ecommerce.Admin.Authentication;

public sealed class AuthenticationService(
    IIdentityApiClient identityApi)
{
    public async Task LoginAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        await identityApi.LoginAsync(
            new LoginRequest(email, password),
            cancellationToken);
    }
}

