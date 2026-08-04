

namespace Ecommerce.Identity.Modules.Infrastructure.Data
{
    internal sealed class IdentityContext: DbContext
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; } = null!;

        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
            
        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new IdentityConfiguration());
            modelBuilder.ApplyConfiguration(new PasswordResetTokenConfiguration());
        }
    }
}
