

namespace Ecommerce.Catalog.Modules.Application.Features.Images.UploadImage;

internal sealed class UploadImageCommandHandler(IFileStorage fileStorage, IFileValidator fileValidator) : ICommandHandler<UploadImageCommand, UploadImageRespnse>
{
    public async Task<Result<UploadImageRespnse>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        var validationResult = fileValidator.Validate(request.FileName, request.ContentType,
            request.Length);
        if (validationResult.IsFailure) 
            return validationResult.Error;
        var storageKey = await fileStorage.UploadAsync(request.Stream,request.FileName,cancellationToken);
        return Result<UploadImageRespnse>.Success(new UploadImageRespnse(storageKey));
      
        
    }
}
