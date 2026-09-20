
using Ecommerce.Notification.Modules.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ecommerce.Notification.Modules.Infrastructure.Database;

internal class NotificationsContext: DbContext
{
    public NotificationsContext(DbContextOptions<NotificationsContext> options): base(options)
    {
        
    }
    public DbSet<Domain.Entities.Notification> Notifications => Set<Domain.Entities.Notification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationsContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
