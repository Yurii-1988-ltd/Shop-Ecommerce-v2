

using Ecommerce.Application.CQRS;
using Ecommerce.Domain.Domain;
using Ecommerce.Localization.Modules.Application.Abstractions;
using System.Globalization;

namespace Ecommerce.Localization.Modules.Application.Features.Translation.CreateTranslation;

public sealed class CreateTranslationCommandHandler(ITranslationRepository repository,
    ILocalizationUnitOfWork unitOfWork) : ICommandHandler<CreateTranslationCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateTranslationCommand request, CancellationToken cancellationToken)
    {
        var cultureResult = Domain.ValueObject.CultureCode.Create(request.CultureCode);
        if (cultureResult.IsFailure)
            return cultureResult.Error;
        var existing = await repository.GetByKeyAsync(request.Key,CultureInfo.GetCultureInfo(cultureResult.Value.Value),cancellationToken);
        if (existing != null)
            return LocalizationErrors.DuplicateTranslation(request.Key,request.CultureCode);
        var result = Domain.Entities.Translation.Create(
            request.Key,
            request.CultureCode,
            request.Value,
            request.Module,
            request.Description);
        if(result.IsFailure)
            return result.Error;
        await repository.AddAsync(result.Value,cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(result.Value.Id);
    }
}
