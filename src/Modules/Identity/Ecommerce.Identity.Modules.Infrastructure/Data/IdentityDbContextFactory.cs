

namespace Ecommerce.Identity.Modules.Infrastructure.Data;

internal sealed class IdentityDbContextFactory : IDesignTimeDbContextFactory<IdentityContext>
{
    public IdentityContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<IdentityContext>();
        builder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=Ecommerce; Integrated Security=true; TrustServerCertificate=true; Encrypt=false;");

        return new IdentityContext(builder.Options);


    }
}
