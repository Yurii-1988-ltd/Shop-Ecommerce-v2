using Ecommerce.Localization.Modules.Domain.Entities;

namespace Ecommerce.Localization.Modules.Infrastructure.Repositories;

internal sealed class LanguageRepository(LocalizationContext context) : ILanguageRepository
{
    public async Task AddAsync(Language language, CancellationToken cancellationToken = default)
    {
         await context.Languages.AddAsync(language, cancellationToken);
    }

    public  Task DeleteAsync(Language language, CancellationToken cancellationToken)
    {
        context.Languages.Remove(language);
        return Task.CompletedTask;
    }

    public async Task<Language?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Languages.FirstOrDefaultAsync(x=>x.Id==id,cancellationToken); 
    }

    public async Task<Language?> GetByCultureCodeAsync(string cultureCode, CancellationToken cancellationToken = default)
    {
        return await context
                .Languages.FirstOrDefaultAsync(x => x.CultureCode == cultureCode, cancellationToken);
    }

    public async Task<Language?> GetDefaultAsync(CancellationToken cancellationToken)
    {
       return await context
            .Languages.FirstOrDefaultAsync(x=>x.IsDefault,cancellationToken);
    }
}
