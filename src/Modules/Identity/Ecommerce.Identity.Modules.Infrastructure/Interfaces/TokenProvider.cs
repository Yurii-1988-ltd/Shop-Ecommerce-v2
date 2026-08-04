



namespace Ecommerce.Identity.Modules.Infrastructure.Interfaces;

internal sealed class TokenProvider : ITokenProvider
{
    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}
