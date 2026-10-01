
using Ecommerce.Application.CQRS;

namespace Ecommerce.Localization.Modules.Application.Features.Language.DisableLanguage;

public sealed record DisableLanguageCommand(Guid Id) : ICommand;

