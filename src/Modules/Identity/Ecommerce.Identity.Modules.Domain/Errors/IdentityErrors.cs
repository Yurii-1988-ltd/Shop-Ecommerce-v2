
using Ecommerce.Domain.Domain;

namespace Ecommerce.Identity.Modules.Domain.Errors
{
    public static class IdentityErrors
    {
        public static Error InvalidResetToken => new(
         "Identity.InvalidResetToken",
         "The password reset token is invalid.",
         ErrorType.Validation);

        public static Error ResetTokenExpired => new(
            "Identity.ResetTokenExpired",
            "The password reset token has expired.",
            ErrorType.Validation);

        public static Error ResetTokenAlreadyUsed => new(
            "Identity.ResetTokenAlreadyUsed",
            "The password reset token has already been used.",
            ErrorType.Validation);

        public static Error InvalidCredentials => new(
            "Identity.InvalidCredentials",
            "The email or password is incorrect.",
            ErrorType.Unauthorized);

        public static Error RefreshTokenExpired => new(
            "Identity.RefreshTokenExpired",
            "The refresh token has expired.",
            ErrorType.Unauthorized);

        public static Error InvalidRefreshToken => new(
            "Identity.InvalidRefreshToken",
            "The refresh token is invalid.",
            ErrorType.Unauthorized);
    }
}
