
using Ecommerce.Application.CQRS;

namespace Ecommerce.Catalog.Modules.Application.Features.Categories.UpdateCategory;

public sealed record UpdateBrandCommand(Guid Id, string Name, string Description) : ICommand<Guid>;

