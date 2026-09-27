

using Ecommerce.Application.CQRS;

namespace Ecommerce.Localization.Modules.Application.Features.Translation.UpdateTranslation
{
    public sealed record UpdateTranslationCommand(Guid Id, string Value, string? Description) : ICommand;
   
}
