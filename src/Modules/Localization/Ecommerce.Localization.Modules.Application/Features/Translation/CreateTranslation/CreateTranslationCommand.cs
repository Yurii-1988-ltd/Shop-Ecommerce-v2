

using Ecommerce.Application.CQRS;

namespace Ecommerce.Localization.Modules.Application.Features.Translation.CreateTranslation;

public sealed record CreateTranslationCommand(string Key, string CultureCode,
                            string Value, string Module, string? Description) : ICommand<Guid>;

