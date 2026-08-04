
using Ecommerce.Application.CQRS;

namespace Ecommerce.Catalog.Modules.Application.Features.Brands.RemoveBrand;

public sealed record RemoveBrandCommand(Guid Id) : ICommand;

