

using Ecommerce.Application.CQRS;
using Ecommerce.Domain.Domain;
using Ecommerce.Localization.Modules.Application.Abstractions;

namespace Ecommerce.Localization.Modules.Application.Features.Language.CreateLanguage
{
    internal sealed class CreateLanguageCommandHandler(ILanguageRepository repository,
                 ILocalizationUnitOfWork unitOfWork) : ICommandHandler<CreateLanguageCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            var existing = await repository.GetByCultureCodeAsync(request.CultureCode,cancellationToken);
            if(existing is not null)
            {
                return LocalizationErrors.DuplicateLanguage(request.CultureCode);
            }
            if(request.IsDefault)
            {
                var defaultLanguage =await repository.GetDefaultAsync(cancellationToken);
                if (defaultLanguage is not null)
                {
                    return LocalizationErrors.DefaultLanguageAlreadyExists;
                }
            }
            
            var result = Domain.Entities.Language.Create(request.CultureCode,
                request.Name, request.NativeName,
                request.IsDefault);
            if (result.IsFailure)
                return result.Error;
            await repository.AddAsync(result.Value, cancellationToken);
            await unitOfWork.SaveChangesAsync();
            return Result.Success(result.Value.Id);
        }
    }
}
