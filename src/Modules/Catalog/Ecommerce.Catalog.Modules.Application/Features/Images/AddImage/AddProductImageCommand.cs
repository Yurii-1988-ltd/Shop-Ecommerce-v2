using Ecommerce.Application.CQRS;

namespace Ecommerce.Catalog.Modules.Application.Features.Images.AddImage;

public record AddProductImageCommand(
    Guid ProductId,
    string StorageKey,
    string? AltText) : ICommand<Guid>;   
