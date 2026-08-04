

using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Order.Modules.Application.Contracts;

public sealed record ProductInfo(Guid Id, string Name, string Sku, Money Price);

