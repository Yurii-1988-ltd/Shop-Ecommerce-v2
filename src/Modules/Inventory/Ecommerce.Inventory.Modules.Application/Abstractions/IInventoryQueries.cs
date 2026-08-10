

using Ecommerce.Inventory.Modules.Application.Responses;

namespace Ecommerce.Inventory.Modules.Application.Abstractions;

public interface IInventoryQueries
{
    Task<IReadOnlyList<InventoryReportItem>> GetReportAsync(
  CancellationToken cancellationToken = default);
}
