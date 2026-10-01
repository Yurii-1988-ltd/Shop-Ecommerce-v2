
using Ecommerce.Application.CQRS;


namespace Ecommerce.Localization.Modules.Application.Features.Language.DeleteLanguage;

public sealed record DeleteLanguageCommand(Guid Id) : ICommand;

