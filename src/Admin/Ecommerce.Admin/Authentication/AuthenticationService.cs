
namespace Ecommerce.Admin.Authentication;

public sealed class AuthenticationService(IIdentityApiClient identityApi,ITokenStorage tokenStorage)
{
    public async Task LoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var response = await identityApi.LoginAsync(new LoginRequest(email, password),cancellationToken);
        await tokenStorage.SetAccessTokenAsync(response.AccessToken);
        await tokenStorage.SetRefreshTokenAsync(response.RefreshToken);
    }
    public async Task LogoutAsync()
    {
        await tokenStorage.RemoveAccessTokenAsync();
        await tokenStorage.RemoveRefreshTokenAsync();
    }

    public Task<string?>GetAccessTokenAsync()=>tokenStorage.GetAccessTokenAsync();
}
