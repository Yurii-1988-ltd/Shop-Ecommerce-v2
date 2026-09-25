internal sealed class NotificationsUnitOfWork(
    NotificationsContext context) : INotificationUnitOfWork
{
    public Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
        => context.SaveChangesAsync(cancellationToken);
}
