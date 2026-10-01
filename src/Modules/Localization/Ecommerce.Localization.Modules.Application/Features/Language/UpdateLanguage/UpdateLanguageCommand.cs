


using Ecommerce.Application.CQRS;

namespace Ecommerce.Localization.Modules.Application.Features.Language.UpdateLanguage;

public sealed record UpdateLanguageCommand(Guid Id,
                                        string CultureCode,
                                        string Name,
                                        string NativeName,
                                        bool IsDefault) : ICommand;

