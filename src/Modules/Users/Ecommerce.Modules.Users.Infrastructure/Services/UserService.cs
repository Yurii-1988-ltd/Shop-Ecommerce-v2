


namespace Ecommerce.Modules.Users.Infrastructure.Services;

internal sealed class UserService(IUserRepository userRepository, IUserUnitOfWork unitOfWork) : IUserService
{
    public async Task<Result<Guid>> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
       var result = User.Create(request.Email, request.PasswordHash, request.FirstName, request.LastName);
        if (result.IsFailure)
        {
            return Result<Guid>.Failure(result.Error);
        }
        var user = result.Value;
        userRepository.Insert(user);
        await unitOfWork.SaveChangesAsync();

        return Result<Guid>.Success(user.Id);
    }

    public async Task<UserAuthenticationResponse?> ConfirmEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(email, cancellationToken);
        if (user is null)
        {
            return null;
        }

        return new UserAuthenticationResponse(user.Id, user.Email, user.PasswordHash);
    }

    public async Task<Result> ChangePasswordAsync(Guid userId, string passwordHash, CancellationToken cancellation)
    {
        var user = await userRepository.GetByIdAsync(userId, cancellation);
        if(user is null)
        {
           return Result.Failure(UserErrors.NotFound(userId));
        }
        var result= user.ChangePassword(passwordHash);
        if (result.IsFailure)
        {
            return result;
        }
        await unitOfWork.SaveChangesAsync();
        return Result.Success();

    }

    public async Task<UserAuthenticationResponse?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(
      email,
      cancellationToken);

        if (user is null)
        {
            return null;
        }

        return new UserAuthenticationResponse(
            user.Id,
            user.Email,
            user.PasswordHash);
    }
}
