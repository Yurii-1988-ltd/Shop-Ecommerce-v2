



namespace Ecommerce.Catalog.Modules.Application.Features.Images.UploadImage;

public sealed record UploadImageCommand(Stream Stream, string FileName, string ContentType,
    long Length) : ICommand<UploadImageRespnse>;

