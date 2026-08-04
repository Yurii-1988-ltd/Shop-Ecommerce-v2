
using Ecommerce.Identity.Modules.Domain.Entities;

namespace Ecommerce.Identity.Modules.Domain.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);
    Task RemoveAllByUserIdAsync(
    Guid userId,
    CancellationToken cancellationToken = default);

    void Insert(RefreshToken refreshToken);
    void Remove(RefreshToken refreshToken);

}
