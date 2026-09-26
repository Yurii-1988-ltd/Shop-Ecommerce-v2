using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace Ecommerce.Localization.Modules.Infrastructure.Database;

internal sealed class LocalizationFactory : IDesignTimeDbContextFactory<LocalizationContext>
{
    public LocalizationContext CreateDbContext(string[] args)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<LocalizationContext>();

        optionsBuilder.UseNpgsql(
     "Host=localhost;Port=5432;Database=localizations;Username=postgres;Password=postgres");

        return new LocalizationContext(optionsBuilder.Options);
    }
}
