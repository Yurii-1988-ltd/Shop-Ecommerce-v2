using Ecommerce.Identity.Modules.Application.Features.Register;
using Ecommerce.Identity.Modules.Domain.Errors;

using Microsoft.Extensions.Options;

namespace Ecommerce.Identity.Modules.Application.Features.Login;

internal sealed class LoginCommandHandler(IUserService userService,
                                           IUserRoleService roleService,
                                            IPasswordHasher passwordHasher,
                                            IJwtProvider jwtProvider,
                                            ITokenProvider tokenProvider,
                                            IRefreshTokenRepository refreshTokenRepository,
                                            IidentityUnitOfWork identityUnitOfWork,
                                            IOptions<JwtOptions> jwtOptions) : ICommandHandler<LoginUserCommand, AuthenticationResponse>
{
    public async Task<Result<AuthenticationResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userService.ConfirmEmailAsync(request.Email, cancellationToken);
        if (user == null)
        {
            return Result<AuthenticationResponse>.Failure(IdentityErrors.InvalidCredentials);
        }
        bool isPasswordValid = passwordHasher.Verify(request.Password, user.PasswordHash);

        if (!isPasswordValid)
        {
            return Result<AuthenticationResponse>.Failure(IdentityErrors.InvalidCredentials);
        }
        var roleResult = await roleService.GetRolesAsync(user.Id, cancellationToken);
        if (roleResult.IsFailure)
            return roleResult.Error;
        var roles = roleResult.Value
            .Select(x=>x.Name)
            .ToArray();
       string accessToken = jwtProvider.Generate(
           user.Id,
           user.Email,
           roles);

      
        string refreshToken = tokenProvider.GenerateRefreshToken();

        var refreshTokenResult = RefreshToken.Create(user.Id,refreshToken, DateTime.UtcNow.AddDays(jwtOptions.Value.RefreshTokenExpirationInDays));
        refreshTokenRepository.Insert(refreshTokenResult.Value);
        await identityUnitOfWork.SaveChangesAsync(cancellationToken);
        return Result<AuthenticationResponse>.Success(new AuthenticationResponse(accessToken, refreshToken));
    }
}
