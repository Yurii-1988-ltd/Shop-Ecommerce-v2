

using Ecommerce.Domain.Domain;
using Ecommerce.Identity.Modules.Domain.Errors;

namespace Ecommerce.Identity.Modules.Domain.Entities;

public class RefreshToken : Entity
{
    public Guid Id { get;private set; }
    public Guid UserId { get;private set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresOnUtc { get;private set; }
    public DateTime? RevokeOnUtc { get; private set; }
    public bool IsExpired => ExpiresOnUtc <= DateTime.UtcNow;
    public bool IsRevoked => RevokeOnUtc.HasValue;

    private RefreshToken() { }


    //static factory method
    public  static Result<RefreshToken> Create(Guid userId, string token, DateTime expiresOnUtc)
    {
        if (userId == Guid.Empty)
            return Result<RefreshToken>.Failure(RefreshTokenErrors.InvalidUserId(userId));
        if(string.IsNullOrWhiteSpace(token))
            return Result<RefreshToken>.Failure(RefreshTokenErrors.TokenRequired(token));
        if(expiresOnUtc<=DateTime.UtcNow)
            return Result<RefreshToken>.Failure(RefreshTokenErrors.InvalidExpirationDate(expiresOnUtc));
        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = token,
            ExpiresOnUtc = expiresOnUtc
        };
        return Result<RefreshToken>.Success(refreshToken);

    }
}
