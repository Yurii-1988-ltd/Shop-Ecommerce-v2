

using Ecommerce.Application.CQRS;

namespace Ecommerce.Localization.Modules.Application.Features.Translation.DeleteTranslation;

public sealed record DeleteTranslationCommand(Guid Id) : ICommand;

