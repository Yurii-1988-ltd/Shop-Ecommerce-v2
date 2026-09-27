

using Ecommerce.Application.CQRS;
using Ecommerce.Domain.Domain;
using Ecommerce.Localization.Modules.Application.Abstractions;

namespace Ecommerce.Localization.Modules.Application.Features.Translation.UpdateTranslation
{
    internal sealed class UpdateTranslationCommandHandler(
        ITranslationRepository repository,
        ILocalizationUnitOfWork unitOfWork) : ICommandHandler<UpdateTranslationCommand>
    {
        public async Task<Result> Handle(
    UpdateTranslationCommand request,
    CancellationToken cancellationToken)
        {
            var translation = await repository.GetByIdAsync(
                request.Id,
                cancellationToken);

            if (translation is null)
            {
                return LocalizationErrors.TranslationNotFound(
                    request.Id);
            }

            translation.Update(
                request.Value,
                request.Description);

            await repository.UpdateAsync(
                translation,
                cancellationToken);

            await unitOfWork.SaveChangesAsync(
                cancellationToken);

            return Result.Success();
        }
    }
}
