
using Ecommerce.Localization.Modules.Domain.Entities;

namespace Ecommerce.Localization.Modules.Infrastructure.Database;

internal static class LocalizationSeeder
{
    public static async Task SeedAsync(
        LocalizationContext context,
        CancellationToken cancellationToken = default)
    {
        if (await context.Languages.AnyAsync(cancellationToken))
            return;

        var languages = new[]
        {
            Language.Create(
                cultureCode: "en-US",
                name: "English",
                nativeName: "English",
                isDefault: true),

            Language.Create(
                cultureCode: "uk-UA",
                name: "Ukrainian",
                nativeName: "Українська"),

            Language.Create(
                cultureCode: "ru-RU",
                name: "Russian",
                nativeName: "Русский")
        };

        var entities = new List<Language>();

        foreach (var result in languages)
        {
            if (result.IsFailure)
                throw new InvalidOperationException(
                    result.Error.Description);

            entities.Add(result.Value);
        }

        await context.Languages.AddRangeAsync(
            entities,
            cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
    }
}