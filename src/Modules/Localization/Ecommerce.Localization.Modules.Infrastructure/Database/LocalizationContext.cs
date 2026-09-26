
using Ecommerce.Localization.Modules.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ecommerce.Localization.Modules.Infrastructure.Database;

internal sealed class LocalizationContext: DbContext
{
    public LocalizationContext(DbContextOptions<LocalizationContext>options): base(options)
    {

        
    }
    public DbSet<Language> Languages => Set<Language>();
    public DbSet<Translation>Translations => Set<Translation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
