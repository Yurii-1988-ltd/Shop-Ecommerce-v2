

using Ecommerce.Inventory.Modules.Application.Responses;

namespace Ecommerce.Inventory.Modules.Application.Features.GetInventoryReport;

public sealed record GetInventoryReportQuery : IQuery<IReadOnlyList<InventoryReportItem>>;

