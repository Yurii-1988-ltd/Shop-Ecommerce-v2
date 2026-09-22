using Ecommerce.Admin.ApiClients.Inventories.Contracts;
using Ecommerce.Admin.ApiClients.Inventories.Responses;
using Ecommerce.Admin.Contracts;

namespace Ecommerce.Admin.ApiClients.Inventories.Api;

internal sealed class InventoryApiClient(
    HttpClient httpClient) : IInventoryApiClient
{
    private const string Inventories = "/inventories";

    public async Task CancelReservationAsync(
        Guid inventoryItemId,
        int quantity,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync(
            $"{Inventories}/{inventoryItemId}/cancel",
            new InventoryQuantityRequest(quantity),
            cancellationToken);
        await EnsureSuccessAsync(response, cancellationToken);
    }

    public async Task CommitReservationAsync(
        Guid inventoryItemId,
        int quantity,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync(
            $"{Inventories}/{inventoryItemId}/commit",
            new InventoryQuantityRequest(quantity),
            cancellationToken);

        await EnsureSuccessAsync(response, cancellationToken);
    }

    public async Task<Guid> CreateAsync(
      CreateInventoryItemRequest request,
      CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync(
            Inventories,
            request,
            cancellationToken);

        await EnsureSuccessAsync(response, cancellationToken);

        return await response.Content.ReadFromJsonAsync<Guid>(
            cancellationToken);
    }

    public async Task DeductAsync(
        Guid inventoryItemId,
        int quantity,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync(
            $"{Inventories}/{inventoryItemId}/deduct",
            new InventoryQuantityRequest(quantity),
            cancellationToken);

      await EnsureSuccessAsync(response, cancellationToken);;
    }

    public async Task<byte[]> ExportExcelAsync(
      CancellationToken cancellationToken = default)
    {
        return await httpClient.GetByteArrayAsync(
            $"{Inventories}/export/excel",
            cancellationToken);
    }

    public async Task<byte[]> ExportPdfAsync(
        CancellationToken cancellationToken = default)
    {
        return await httpClient.GetByteArrayAsync(
            $"{Inventories}/export/pdf",
            cancellationToken);
    }

    public async Task<IReadOnlyList<InventoryReportItemResponse>>
        GetInventoryReportAsync(
            CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<
            IReadOnlyList<InventoryReportItemResponse>>(
                $"{Inventories}/report",
                cancellationToken)
            ?? [];
    }

    public async Task ReplenishStockAsync(
        Guid inventoryItemId,
        int quantity,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync(
            $"{Inventories}/{inventoryItemId}/replenish",
            new InventoryQuantityRequest(quantity),
            cancellationToken);

      await EnsureSuccessAsync(response, cancellationToken);
    }

    public async Task ReserveStockAsync(
        Guid inventoryItemId,
        int quantity,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync(
            $"{Inventories}/{inventoryItemId}/reserve",
            new InventoryQuantityRequest(quantity),
            cancellationToken);

        await EnsureSuccessAsync(response, cancellationToken);
    }
    private async Task EnsureSuccessAsync(
    HttpResponseMessage response,
    CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode)
            return;

        var error = await response.Content.ReadFromJsonAsync<ApiError>(
            cancellationToken);

        if (error != null)
            throw new ApiException(error);

        throw new HttpRequestException(
            $"Request failed with status code {response.StatusCode}");
    }
}