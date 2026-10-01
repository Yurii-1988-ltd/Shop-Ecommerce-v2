using Ecommerce.Application.CQRS;
using Ecommerce.Domain.Domain;
using Ecommerce.Localization.Modules.Application.Abstractions;
using System.Globalization;

namespace Ecommerce.Localization.Modules.Application.Features.Translation.CreateTranslation;

public sealed class CreateTranslationCommandHandler(
    ITranslationRepository repository,
    ILocalizationUnitOfWork unitOfWork)
    : ICommandHandler<CreateTranslationCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateTranslationCommand request,
        CancellationToken cancellationToken)
    {
        var cultureResult =
            Domain.ValueObject.CultureCode.Create(request.CultureCode);

        if (cultureResult.IsFailure)
            return cultureResult.Error;

        var cultureCode = cultureResult.Value.Value;

        var existing = await repository.GetByKeyAsync(
            request.Key,
            CultureInfo.GetCultureInfo(cultureCode),
            request.Module,
            cancellationToken);

        if (existing is not null)
            return LocalizationErrors.DuplicateTranslation(
                request.Key,
                cultureCode);

        var translationResult = Domain.Entities.Translation.Create(
            request.Key,
            cultureCode,
            request.Value,
            request.Module,
            request.Description);

        if (translationResult.IsFailure)
            return translationResult.Error;

        await repository.AddAsync(
            translationResult.Value,
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(translationResult.Value.Id);
    }
}