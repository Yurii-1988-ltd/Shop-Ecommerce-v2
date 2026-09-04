

using Ecommerce.Domain.Domain;

namespace Ecommerce.Identity.Modules.Domain.Errors;

public static class RefreshTokenErrors
{
    public static Error InvalidUserId(Guid userId)
        => new Error(
            "RefreshToken.InvalidUserId",
            $"Invalid user id {userId}",
            ErrorType.Validation);
    public static Error TokenRequired(string token)
        => new Error(
            "RefreshToken.TokenRequired",
            $"Token is required",
            ErrorType.Validation);
    public static Error InvalidExpirationDate(DateTime expiresOnUtc)
        => new Error(
            "RefreshToken.InvalidExpirationDate",
            $"Invalid expiration date {expiresOnUtc}",
            ErrorType.Validation);
    public static Error NotFound
    => new Error(
        "RefreshToken.NotFound",
        "Refresh token was not found.",
        ErrorType.NotFound);

    public static Error Expired
        => new Error(
            "RefreshToken.Expired",
            "Refresh token has expired.",
            ErrorType.Validation);

    public static Error Revoked
        => new Error(
            "RefreshToken.Revoked",
            "Refresh token has been revoked.",
            ErrorType.Validation);
}
