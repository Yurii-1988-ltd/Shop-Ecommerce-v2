

using Ecommerce.Application.CQRS;
using Ecommerce.Domain.Domain;
using Ecommerce.Localization.Modules.Application.Abstractions;

namespace Ecommerce.Localization.Modules.Application.Features.Language.EnableLanguage;

internal sealed class EnableLanguageCommandHandler(ILanguageRepository repository,
    ILocalizationUnitOfWork unitOfWork) : ICommandHandler<EnableLanguageCommand>
{
    public async Task<Result> Handle(EnableLanguageCommand request, CancellationToken cancellationToken)
    {
        var languge = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (languge is null)
            return LocalizationErrors.LanguageNotFound(request.Id);
        languge.Enable();
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
