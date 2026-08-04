

using Ecommerce.Application.Abstractions;

namespace Ecommerce.Catalog.Modules.Infrastructure.Services;

internal class ProductNumberGenerator(IProductRepository repository) : IEntityNumberGenerator
{
    public async Task<string> GenerateAsync(string prefix, CancellationToken cancellationToken = default)
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

