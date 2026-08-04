

using Ecommerce.Identity.Modules.Domain.Entities;

namespace Ecommerce.Identity.Modules.Domain.Repositories;

public interface IPasswordResetTokenRepository
{
    Task<PasswordResetToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PasswordResetToken>> GetActiveByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    void Insert(PasswordResetToken token);
    void Remove(PasswordResetToken token);
}
