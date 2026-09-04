
using Microsoft.Extensions.Options;

namespace Ecommerce.Identity.Modules.Application.Features.Register;

public sealed class RegisterUserCommandHandler(IUserService userService,
    IPasswordHasher passwordHasher,
    IJwtProvider jwtProvider,
    ITokenProvider tokenProvider,
    IOptions<JwtOptions> jwtOptions,
    IRefreshTokenRepository refreshTokenRepository,
    IidentityUnitOfWork unitOfWork)
    : ICommandHandler<RegisterUserCommand, AuthenticationResponse>
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    public async Task<Result<AuthenticationResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        string passwordHash = passwordHasher.Hash(request.Password);
        //Create User
        Result<Guid> createUserResult = await userService
            .CreateAsync(new(request.Email, passwordHash, request.FirstName, request.LastName), cancellationToken);
        if (createUserResult.IsFailure)
        {
            return Result<AuthenticationResponse>.Failure(createUserResult.Error);
        }
        //Generate Tokens
        string accessToken = jwtProvider.Generate(createUserResult.Value, request.Email,Array.Empty<string>());
        //Generate Refresh Token
        string refreshToken = tokenProvider.GenerateRefreshToken();
        //Generate Refresh Token Entity

        var refreshTokenResult = Domain.Entities. RefreshToken.Create(
            createUserResult.Value,
            refreshToken,
            DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpirationInDays));
        if (refreshTokenResult.IsFailure)
        {
            return Result<AuthenticationResponse>.Failure(refreshTokenResult.Error);
        }
        refreshTokenRepository.Insert(refreshTokenResult.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<AuthenticationResponse>.Success(new AuthenticationResponse(accessToken, refreshToken));

    }
}
