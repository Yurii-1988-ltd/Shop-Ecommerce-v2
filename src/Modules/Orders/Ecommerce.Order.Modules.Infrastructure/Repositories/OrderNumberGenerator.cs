using Ecommerce.Application.Abstractions;

namespace Ecommerce.Order.Modules.Infrastructure.Services;

internal sealed class OrderNumberGenerator(
    IOrderRepository repository)
    : IEntityNumberGenerator
{
    public async Task<string> GenerateAsync(
        string prefix,
        CancellationToken cancellationToken = default)
    {
        while (true)
        {
            var number = $"{prefix}-{DateTime.UtcNow:yyyy}-{Random.Shared.Next(1, 999999):D6}";

            if (!await repository.ExistOrderNumberAsync(number, cancellationToken))
            {
                return number;
            }
        }
    }
}