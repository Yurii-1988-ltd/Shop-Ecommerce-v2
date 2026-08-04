

using Ecommerce.Domain.Domain;

namespace Ecommerce.Identity.Modules.Domain.Errors;

internal static class PasswordResetTokenErrors
{
    public static Error InvalidUserId(Guid userId)
        => new Error(
            "PasswordResetToken.InvalidUserId",
            $"Invalid user id '{userId}'",ErrorType.Validation);
    public static Error TokenRequired => new Error(
        "PasswordResetToken.TokenRequired",
        "Token is required",
        ErrorType.Validation);
    public static Error ExpiresOnUtcMustBeInTheFuture(DateTime expirationDate)
        => new Error(
            "PasswordResetToken.ExpiresOnUtcMustBeInTheFuture",
            $"ExpiresOnUtc must be in the future. Current date: {DateTime.UtcNow}, Provided date: {expirationDate}",
            ErrorType.Validation);
    public static Error AlreadyUsed(Guid tokenId)
        => new Error(
            "PasswordResetToken.AlreadyUsed",
            $"Password reset token with id '{tokenId}' has already been used",
            ErrorType.Validation);
    public static Error AlreadyRevoked(Guid tokenId)
        => new Error(
            "PasswordResetToken.AlreadyRevoked",
            $"Password reset token with id '{tokenId}' has already been revoked",
            ErrorType.Validation);
    public static Error TokenExpired(Guid tokenId)
    => new(
        "PasswordResetToken.TokenExpired",
        $"Password reset token '{tokenId}' has expired.",
        ErrorType.Validation);
}
