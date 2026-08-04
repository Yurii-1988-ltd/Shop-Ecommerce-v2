

using Ecommerce.Domain.Domain;
using Ecommerce.Identity.Modules.Domain.Errors;

namespace Ecommerce.Identity.Modules.Domain.Entities;

public sealed class PasswordResetToken : Entity
{
    private PasswordResetToken()
    {
        
    }
    public Guid Id { get; private set; }
    public Guid UserId { get;private set; }
    public string Token { get; set; } = string.Empty;
    public DateTime? UsedOnUtc { get; private set; }

    public DateTime? RevokedOnUtc { get; private set; }
    public DateTime ExpiresOnUtc { get; private set; }

    public bool IsExpired => ExpiresOnUtc <= DateTime.UtcNow;

    public bool IsUsed => UsedOnUtc.HasValue;

    public bool IsRevoked => RevokedOnUtc.HasValue;

    public bool IsActive =>
        !IsExpired &&
        !IsUsed &&
        !IsRevoked;
    public static Result<PasswordResetToken> Create(
     Guid userId,
     string token,
     DateTime expiresOnUtc)
    {
        if (userId == Guid.Empty)
        {
            return PasswordResetTokenErrors.InvalidUserId(userId);
        }

        if (string.IsNullOrWhiteSpace(token))
        {
            return PasswordResetTokenErrors.TokenRequired;
        }

        if (expiresOnUtc <= DateTime.UtcNow)
        {
            return PasswordResetTokenErrors.ExpiresOnUtcMustBeInTheFuture(expiresOnUtc);
        }

        var passwordResetToken = new PasswordResetToken
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = token,
            ExpiresOnUtc = expiresOnUtc
        };

        return passwordResetToken;
    }
    public Result MarkAsUsed()
    {
        if (IsUsed)
        {
            return Result.Failure(PasswordResetTokenErrors.AlreadyUsed(Id));
        }
        UsedOnUtc = DateTime.UtcNow;
        return Result.Success();
    }
    public Result Revoke()
    {
        if(IsRevoked)
        {
            return Result.Failure(PasswordResetTokenErrors.AlreadyRevoked(Id));
        }
        RevokedOnUtc = DateTime.UtcNow;
        return Result.Success();
    }
}
