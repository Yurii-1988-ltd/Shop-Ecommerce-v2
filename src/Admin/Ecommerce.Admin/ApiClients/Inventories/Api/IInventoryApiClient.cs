using Ecommerce.Admin.ApiClients.Inventories.Contracts;
using Ecommerce.Admin.ApiClients.Inventories.Responses;

namespace Ecommerce.Admin.ApiClients.Inventories.Api;

public interface IInventoryApiClient
{
    Task<IReadOnlyList<InventoryReportItemResponse>> GetInventoryReportAsync(
        CancellationToken cancellationToken = default);

    Task<Guid> CreateAsync(
        CreateInventoryItemRequest request,
        CancellationToken cancellationToken = default);

    Task CancelReservationAsync(
        Guid inventoryItemId,
        int quantity,
        CancellationToken cancellationToken = default);

    Task CommitReservationAsync(
        Guid inventoryItemId,
        int quantity,
        CancellationToken cancellationToken = default);

    Task DeductAsync(
        Guid inventoryItemId,
        int quantity,
        CancellationToken cancellationToken = default);

    Task ReplenishStockAsync(
        Guid inventoryItemId,
        int quantity,
        CancellationToken cancellationToken = default);

    Task ReserveStockAsync(
        Guid inventoryItemId,
        int quantity,
        CancellationToken cancellationToken = default);
    Task<byte[]> ExportExcelAsync(
    CancellationToken cancellationToken = default);

    Task<byte[]> ExportPdfAsync(
        CancellationToken cancellationToken = default);
}