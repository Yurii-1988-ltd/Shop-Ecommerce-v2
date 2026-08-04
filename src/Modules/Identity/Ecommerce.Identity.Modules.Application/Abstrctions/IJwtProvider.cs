

namespace Ecommerce.Identity.Modules.Application.Abstractions;

/// <summary>
/// Provides functionality for generating JSON Web Tokens (JWTs) for user authentication and authorization.
/// </summary>
public interface IJwtProvider
{
    string Generate(Guid userId, string email,
        IEnumerable<string> roles);
}
