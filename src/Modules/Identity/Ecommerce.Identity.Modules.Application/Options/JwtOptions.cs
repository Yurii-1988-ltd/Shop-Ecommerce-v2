

namespace Ecommerce.Identity.Modules.Application.Options;

public class JwtOptions
{
    public const string SectionName = "Jwt";
    public string SecretKey { get; init; } = null!;
   public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public int ExpirationInMinutes { get; init; }
    public int RefreshTokenExpirationInDays { get; init; } 
}
