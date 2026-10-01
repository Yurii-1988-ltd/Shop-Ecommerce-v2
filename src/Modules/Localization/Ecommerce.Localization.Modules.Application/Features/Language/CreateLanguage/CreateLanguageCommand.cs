
using Ecommerce.Application.CQRS;

namespace Ecommerce.Localization.Modules.Application.Features.Language.CreateLanguage;

public sealed record CreateLanguageCommand(
                string CultureCode,
                string Name,
                string NativeName,
                bool IsDefault) : ICommand<Guid>;

