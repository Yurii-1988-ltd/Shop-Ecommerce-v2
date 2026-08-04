
using BCrypt.Net;
using Ecommerce.Application.Abstractions.Security;

namespace Ecommerce.Modules.Users.Infrastructure.Repositories;

public sealed class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);

    public bool Verify(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}