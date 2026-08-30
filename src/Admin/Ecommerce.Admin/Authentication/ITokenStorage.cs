namespace Ecommerce.Admin.Authentication
{
    public interface ITokenStorage
    {
        ValueTask SetAccessTokenAsync(string token);
        Task<string?> GetAccessTokenAsync();
        ValueTask RemoveAccessTokenAsync();
        Task SetRefreshTokenAsync(string token);
        Task<string?> GetRefreshTokenAsync();
        ValueTask RemoveRefreshTokenAsync();
    }
}
