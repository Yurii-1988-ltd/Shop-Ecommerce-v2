

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
            $"Token is required {token}",
            ErrorType.Validation);
    public static Error InvalidExpirationDate(DateTime expiresOnUtc)
        => new Error(
            "RefreshToken.InvalidExpirationDate",
            $"Invalid expiration date {expiresOnUtc}",
            ErrorType.Validation);
}
