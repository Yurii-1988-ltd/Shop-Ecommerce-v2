

using Ecommerce.Application.CQRS;
using Ecommerce.Domain.Domain;
using Ecommerce.Localization.Modules.Application.Abstractions;

namespace Ecommerce.Localization.Modules.Application.Features.Language.DeleteLanguage;

internal sealed class DeleteLanguageCommandHandler(ILanguageRepository repository,
                                                ILocalizationUnitOfWork unitOfWork)
                                                : ICommandHandler<DeleteLanguageCommand>
{
    public async Task<Result> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
    {
        var language = await repository.GetByIdAsync(
           request.Id,
           cancellationToken);

        if (language is null)
            return LocalizationErrors.LanguageNotFound(request.Id);

        if (language.IsDefault)
            return LocalizationErrors.CannotDeleteDefaultLanguage;

        await repository.DeleteAsync(
            language,
            cancellationToken);

        await unitOfWork.SaveChangesAsync(
            cancellationToken);

        return Result.Success();
    }
}
