
namespace Ecommerce.Identity.Modules.Infrastructure.Repositories;

internal sealed class RefreshTokenRepository(IdentityContext context) : IRefreshTokenRepository
{
    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        return await context.Set<RefreshToken>().SingleOrDefaultAsync(rt => rt.Token == token, cancellationToken);
    }

    public async Task RemoveAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var tokens = await context.RefreshTokens
         .Where(rt => rt.UserId == userId)
         .ToListAsync(cancellationToken);

        context.RefreshTokens.RemoveRange(tokens);
    }

    public void Insert(RefreshToken refreshToken)
    {
      context.Set<RefreshToken>().Add(refreshToken);
    }

    public void Remove(RefreshToken refreshToken)
    {
        context.Set<RefreshToken>().Remove(refreshToken);
    }
}
    

