

namespace Ecommerce.Identity.Modules.Infrastructure.Repositories
{
    internal sealed class PasswordResetTokenRepository(IdentityContext context) : IPasswordResetTokenRepository
    {
        public async Task<IReadOnlyList<PasswordResetToken>> GetActiveByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await context.PasswordResetTokens
                .Where(t => t.UserId == userId &&
                t.UsedOnUtc==null &&
                t.RevokedOnUtc==null &&
                t.ExpiresOnUtc > DateTime.UtcNow)
                .ToListAsync(cancellationToken);
        }

        public async Task<PasswordResetToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            return await context.PasswordResetTokens
                .FirstOrDefaultAsync(t => t.Token == token, cancellationToken);
        }

        public void Insert(PasswordResetToken token)
        {
            context.PasswordResetTokens.Add(token);
            

        }

        public void Remove(PasswordResetToken token)
        {
            context.PasswordResetTokens.Remove(token);
           
        }
    }
}
