

using Ecommerce.Application.CQRS;
using Ecommerce.Domain.Domain;
using Ecommerce.Localization.Modules.Application.Abstractions;

namespace Ecommerce.Localization.Modules.Application.Features.Translation.DeleteTranslation;

internal sealed class DeleteTranslationCommandHandler(ITranslationRepository repository,
                                    ILocalizationUnitOfWork unitOfWork) : ICommandHandler<DeleteTranslationCommand>
{
    public async Task<Result> Handle(DeleteTranslationCommand request, CancellationToken cancellationToken)
    {
        var translation = await repository.GetByIdAsync(request.Id, cancellationToken);
        if(translation is null)
        {
            return LocalizationErrors.TranslationNotFound(request.Id);
        }
        await repository.DeleteAsync(translation, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
       
    }
}