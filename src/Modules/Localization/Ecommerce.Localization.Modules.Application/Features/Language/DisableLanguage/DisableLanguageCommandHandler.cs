using Ecommerce.Application.CQRS;
using Ecommerce.Domain.Domain;
using Ecommerce.Localization.Modules.Application.Abstractions;
using Ecommerce.Localization.Modules.Application.Features.Language.DisableLanguage;

namespace Ecommerce.Localization.Modules.Application.Features.Language.EnableLanguage;

internal sealed class DisableLanguageCommandHandler(ILanguageRepository repository,
    ILocalizationUnitOfWork unitOfWork) : ICommandHandler<DisableLanguageCommand>
{
    public async Task<Result> Handle(DisableLanguageCommand request, CancellationToken cancellationToken)
    {
        var languge = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (languge is null)
            return LocalizationErrors.LanguageNotFound(request.Id);
        languge.Disable();
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}