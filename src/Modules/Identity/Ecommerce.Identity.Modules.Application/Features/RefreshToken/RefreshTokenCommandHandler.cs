

using Ecommerce.Identity.Modules.Application.Dto;
using Ecommerce.Identity.Modules.Domain.Errors;
using Microsoft.Extensions.Options;

namespace Ecommerce.Identity.Modules.Application.Features.RefreshToken;

internal sealed class RefreshTokenCommandHandler(
    IRefreshTokenRepository refreshTokenRepository,
    IUserService userService,
    IUserRoleService roleService,
    IJwtProvider jwtProvider,
    ITokenProvider tokenProvider,
    IidentityUnitOfWork unitOfWork,
    IOptions<JwtOptions> jwtOptions
    ) : ICommandHandler<RefreshTokenCommand, AuthenticationResponse>
{
    public async Task<Result<AuthenticationResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await refreshTokenRepository.GetByTokenAsync(request.RefreshToken, cancellationToken);
        if(refreshToken is null)
        {
            return Result<AuthenticationResponse>.Failure(RefreshTokenErrors.NotFound);
        }
        if(refreshToken.IsExpired)
        {
            return Result<AuthenticationResponse>.Failure(RefreshTokenErrors.Expired);
        }
        if(refreshToken.IsRevoked)
        {
            return Result<AuthenticationResponse>.Failure(RefreshTokenErrors.Revoked);
        }
        var user = await userService.GetByIdAsync(refreshToken.UserId, cancellationToken);
        if (user is null)
        {
            return Result<AuthenticationResponse>.Failure(RefreshTokenErrors.NotFound);
        }
        var roleResult= await roleService.GetRolesAsync(user.Id, cancellationToken);
        if (roleResult.IsFailure)
        {
            return Result<AuthenticationResponse>.Failure(roleResult.Error);
        }
        var roles = roleResult.Value
            .Select(x => x.Name)
            .ToArray();
        var accessToken = jwtProvider.Generate(user.Id,user.Email, roles);
        var newRefreshTokenValue = tokenProvider.GenerateRefreshToken();
        var newRefreshTokenResult = Domain.Entities.RefreshToken.Create(
                        user.Id,
                        newRefreshTokenValue,
                        DateTime.UtcNow.AddDays(jwtOptions.Value.RefreshTokenExpirationInDays)) ;
        if (newRefreshTokenResult.IsFailure)
        {
            return Result<AuthenticationResponse>.Failure(newRefreshTokenResult.Error);
        }
        refreshToken.Revoke();
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<AuthenticationResponse>.Success(
            new AuthenticationResponse(accessToken, newRefreshTokenValue));




    }
}
