using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ecommerce.Modules.Users.Infrastructure.Data
{
    internal sealed class UserDbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        public UserDbContext CreateDbContext(string[] args)
        {            var builder = new DbContextOptionsBuilder<UserDbContext>();
            builder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=Ecommerce; Integrated Security=true; TrustServerCertificate=true; Encrypt=false;");

            return new UserDbContext(builder.Options);


        }
    }
}
