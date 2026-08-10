using Ecommerce.Inventory.Modules.Application.Abstractions;
using Ecommerce.Inventory.Modules.Application.Responses;

namespace Ecommerce.Inventory.Modules.Application.Features.GetInventoryReport;

internal sealed class GetInventoryReportQueryHandler(
    IInventoryQueries queries)
    : IQueryHandler<
        GetInventoryReportQuery,
        IReadOnlyList<InventoryReportItem>>
{
    public async Task<Result<IReadOnlyList<InventoryReportItem>>> Handle(
        GetInventoryReportQuery request,
        CancellationToken cancellationToken)
    {
        var items = await queries.GetReportAsync(cancellationToken);

        return Result.Success<IReadOnlyList<InventoryReportItem>>(items);
    }
}