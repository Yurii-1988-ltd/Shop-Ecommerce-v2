using Ecommerce.Application.CQRS;
using Ecommerce.Domain.Domain;
using Ecommerce.Localization.Modules.Application.Abstractions;
using Ecommerce.Localization.Modules.Application.Features.Language.UpdateLanguage;
namespace Ecommerce.Localization.Modules.Application.Features.Languages.UpdateLanguage;

internal sealed class UpdateLanguageCommandHandler(
    ILanguageRepository repository,
    ILocalizationUnitOfWork unitOfWork)
    : ICommandHandler<UpdateLanguageCommand>
{
    public async Task<Result> Handle(
        UpdateLanguageCommand request,
        CancellationToken cancellationToken)
    {
        var language = await repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (language is null)
            return LocalizationErrors.LanguageNotFound(request.Id);

        var existing = await repository.GetByCultureCodeAsync(
            request.CultureCode,
            cancellationToken);

        if (existing is not null && existing.Id != language.Id)
        {
            return LocalizationErrors.DuplicateLanguage(
                request.CultureCode);
        }

        // CultureCode
        var cultureResult = language.ChangeCultureCode(
            request.CultureCode);

        if (cultureResult.IsFailure)
            return cultureResult.Error;

        // Name
        var nameResult = language.ChangeName(
            request.Name);

        if (nameResult.IsFailure)
            return nameResult.Error;

        // NativeName
        var nativeNameResult = language.ChangeNativeName(
            request.NativeName);

        if (nativeNameResult.IsFailure)
            return nativeNameResult.Error;

        // Default language
        if (request.IsDefault && !language.IsDefault)
        {
            var currentDefault = await repository.GetDefaultAsync(
                cancellationToken);

            if (currentDefault is not null &&
                currentDefault.Id != language.Id)
            {
                currentDefault.RemoveAsDefault();
            }

            language.SetAsDefault();
        }
        else if (!request.IsDefault && language.IsDefault)
        {
            language.RemoveAsDefault();
        }

        await unitOfWork.SaveChangesAsync(
            cancellationToken);

        return Result.Success();
    }
}