

using System.Security.Cryptography;

namespace Ecommerce.Identity.Modules.Application.Features.ForgotPassword;

internal sealed class ForgotPassworCommandHandler(IUserService userService,
    IPasswordResetTokenRepository passwordResetTokenRepository,
    IidentityUnitOfWork unitOfWork) : ICommandHandler<ForgotPasswordCommand>
{
    public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userService.GetByEmailAsync(request.Email,cancellationToken);
        if (user is null)
        {
            return Result.Success();
        }
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64) );
        var result = PasswordResetToken.Create(user.Id, token, DateTime.UtcNow.AddHours(1));

        if(result.IsFailure)
        {
            return result.Error;
        }
        passwordResetTokenRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync();

        // TODO send by email
        return Result.Success();
    }
}
