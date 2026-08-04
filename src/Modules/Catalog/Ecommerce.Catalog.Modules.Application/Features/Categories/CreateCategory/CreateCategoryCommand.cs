

using Ecommerce.Application.CQRS;

namespace Ecommerce.Catalog.Modules.Application.Features.Categories.CreateCategory;

public sealed record CreateCategoryCommand(string Name, string Description)
                : ICommand<Guid>;

