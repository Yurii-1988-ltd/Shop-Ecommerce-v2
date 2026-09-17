

using Ecommerce.Inventory.Modules.Application.Features.Responses;

namespace Ecommerce.Inventory.Modules.Application.Features.GetInventoryAvailability;

public sealed record GetInventoryAvailabilityQuery(Guid ProductId):IQuery<InventoryAvailabilityResponse>;

