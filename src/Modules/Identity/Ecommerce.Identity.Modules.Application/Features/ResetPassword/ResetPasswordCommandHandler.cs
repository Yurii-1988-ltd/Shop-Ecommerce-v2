

using Ecommerce.Identity.Modules.Domain.Errors;

namespace Ecommerce.Identity.Modules.Application.Features.ResetPassword;

internal sealed class ResetPasswordCommandHandler(IUserService userService, 
    IPasswordResetTokenRepository passwordResetTokenRepository,
                                IRefreshTokenRepository refreshTokenRepository,
                                IPasswordHasher passwordHasher,
                                IidentityUnitOfWork unitOfWork) : ICommandHandler<ResetPasswordCommand>
 {
    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var resetToken = await passwordResetTokenRepository.GetByTokenAsync(request.Token, cancellationToken);
        if (resetToken == null)
        {
           return IdentityErrors.InvalidResetToken;
        }
        if (resetToken.IsExpired)
        {
            return IdentityErrors.ResetTokenExpired;
        }
        // get user
        var user = await  userService.GetByEmailAsync(request.Email, cancellationToken);

        if(user is null)
        {
            return IdentityErrors.InvalidCredentials;
        }
        var passwordHash = passwordHasher.Hash(request.newPassword);
        var result = await userService.ChangePasswordAsync(user.Id, passwordHash, cancellationToken);
        if(result.IsFailure)
        {
            return result;
        }
        // Remove RefreshToken
        await refreshTokenRepository.RemoveAllByUserIdAsync(user.Id,cancellationToken);
        resetToken.MarkAsUsed();
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
