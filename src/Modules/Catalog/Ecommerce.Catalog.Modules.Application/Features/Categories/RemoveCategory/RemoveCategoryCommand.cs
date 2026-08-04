
using Ecommerce.Application.CQRS;

namespace Ecommerce.Catalog.Modules.Application.Features.Categories.RemoveCategory;

public sealed record RemoveCategoryCommand(Guid Id) : ICommand;

