using Microsoft.JSInterop;

namespace Ecommerce.Admin.Authentication
{
    internal sealed class TokenStorage(IJSRuntime jS) : ITokenStorage
    {
        private const string AccessTokenKey = "accessToken";
        private const string RefreshTokenKey = "refreshToken";
        public async Task<string?> GetAccessTokenAsync()
            => await jS.InvokeAsync<string?>("localStorage.getItem", AccessTokenKey);


        public async Task<string?> GetRefreshTokenAsync()
        => await jS.InvokeAsync<string?>("localStorage.getItem", AccessTokenKey);

        public ValueTask RemoveAccessTokenAsync()
            => jS.InvokeVoidAsync("localStorage.removeItem", AccessTokenKey);


        public ValueTask RemoveRefreshTokenAsync()
            => jS.InvokeVoidAsync("localStorage.removeItem",
                RefreshTokenKey);
        

        public ValueTask SetAccessTokenAsync(string token)
        => jS.InvokeVoidAsync("localStorage.getItem",AccessTokenKey,token);

        public Task SetRefreshTokenAsync(string token)
        {
            throw new NotImplementedException();
        }
    }
}
