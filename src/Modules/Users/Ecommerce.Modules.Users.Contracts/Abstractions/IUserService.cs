
using Ecommerce.Domain.Domain;
using Ecommerce.Modules.Users.Contracts.Requests;

namespace Ecommerce.Modules.Users.Contracts.Abstractions;

public interface IUserService
{
    /// <summary>
    /// Creates a new user and persists it.
    /// </summary>
    /// <param name="request">The user creation request.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing the created user's identifier if successful;
    /// otherwise, an error describing the failure.
    /// </returns>
    Task<Result<Guid>> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken);
    Task<UserAuthenticationResponse?>ConfirmEmailAsync(string email, CancellationToken cancellationToken);
    Task<Result> ChangePasswordAsync(Guid userId, string passwordHash, CancellationToken cancellation);
    Task<UserAuthenticationResponse?> GetByEmailAsync(
    string email,
    CancellationToken cancellationToken);
    Task<UserAuthenticationResponse?> GetByIdAsync(Guid userId, CancellationToken cancellationToken);

}
