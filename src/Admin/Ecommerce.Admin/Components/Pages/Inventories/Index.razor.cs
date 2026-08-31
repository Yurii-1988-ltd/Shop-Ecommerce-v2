using Ecommerce.Admin.ApiClients.Inventories.Responses;
using Ecommerce.Admin.Components.Pages.Inventories.Dialogs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace Ecommerce.Admin.Components.Pages.Inventories;

public partial class Index
{
    private List<InventoryReportItemResponse> _inventories = new();

    protected override async Task OnInitializedAsync()
    {
        await LoadInventory();
    }

    private async Task LoadInventory()
    {
        var result = await InventoryApi.GetInventoryReportAsync();
        _inventories = result.ToList();
    }

    // 1. Replenish (Показываем On Hand)
    private Task Replenish(InventoryReportItemResponse inventory) =>
     ExecuteQuantityDialogAsync(
         title: "Replenish Inventory",
         actionText: "Replenish",
         icon: Icons.Material.Filled.Add,
         inventory: inventory,
         quantityLabel: "On Hand",
         currentQuantity: inventory.OnHandQuantity,
         apiAction: (id, q) => InventoryApi.ReplenishStockAsync(id, q));
    // 2. Deduct (Показываем On Hand)
    private Task Deduct(InventoryReportItemResponse inventory) =>
     ExecuteQuantityDialogAsync(
         title: "Deduct Inventory",
         actionText: "Deduct",
         icon: Icons.Material.Filled.Remove,
         inventory: inventory,
         quantityLabel: "On Hand",
         currentQuantity: inventory.OnHandQuantity,
         apiAction: (id, q) => InventoryApi.DeductAsync(id, q));

    // 3. Reserve (Показываем Available)
    private Task Reserve(InventoryReportItemResponse inventory) =>
    ExecuteQuantityDialogAsync(
        title: "Reserve Inventory",
        actionText: "Reserve",
        icon: Icons.Material.Filled.Lock,
        inventory: inventory,
        quantityLabel: "Available",
        currentQuantity: inventory.AvailableQuantity,
        apiAction: (id, q) => InventoryApi.ReserveStockAsync(id, q));

    // 4. Cancel Reservation (Показываем Reserved)
    private Task CancelReservation(InventoryReportItemResponse inventory) =>
     ExecuteQuantityDialogAsync(
         title: "Cancel Reservation",
         actionText: "Cancel Reservation",
         icon: Icons.Material.Filled.LockOpen,
         inventory: inventory,
         quantityLabel: "Reserved",
         currentQuantity: inventory.ReservedQuantity,
         apiAction: (id, q) => InventoryApi.CancelReservationAsync(id, q));

    // 5. Commit Reservation (Показываем Reserved)
    private Task CommitReservation (InventoryReportItemResponse inventory) =>
     ExecuteQuantityDialogAsync(
         title: "Commit Reservation",
         actionText: "Commit",
         icon: Icons.Material.Filled.CheckCircle,
         inventory: inventory,
         quantityLabel: "Reserved",
         currentQuantity: inventory.ReservedQuantity,
         apiAction: (id, q) => InventoryApi.CommitReservationAsync(id, q));

    // Универсальный метод вызова диалога
    private async Task ExecuteQuantityDialogAsync(
        string title,
        string actionText,
        string icon,
        InventoryReportItemResponse inventory,
        string quantityLabel,
        int currentQuantity,
        Func<Guid, int, Task> apiAction)
    {
        var parameters = new DialogParameters
        {
            { nameof(InventoryQuantityDialog.Title), title },
            { nameof(InventoryQuantityDialog.ActionText), actionText },
            { nameof(InventoryQuantityDialog.Icon), icon },
            { nameof(InventoryQuantityDialog.SKU), inventory.SKU },
            { nameof(InventoryQuantityDialog.QuantityLabel), quantityLabel },
            { nameof(InventoryQuantityDialog.CurrentQuantity), currentQuantity } // <- Явная передача значения!
        };

        var dialog = await DialogService.ShowAsync<InventoryQuantityDialog>(title, parameters);
        var result = await dialog.Result;

        if (result is null || result.Canceled)
            return;

        var quantity = (int)result.Data!;
        await apiAction(inventory.InventoryItemId, quantity);
        await LoadInventory();
    }

    private Task ExportExcel() =>
     ExportFileAsync(
         "inventories.xlsx",
         "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
         ct => InventoryApi.ExportExcelAsync(ct));

    private Task ExportPdf() =>
        ExportFileAsync(
            "inventories.pdf",
            "application/pdf",
            ct => InventoryApi.ExportPdfAsync(ct));

    private async Task ExportFileAsync(
        string fileName,
        string contentType,
        Func<CancellationToken, Task<byte[]>> exportFunc)
    {
        try
        {
            // При необходимости можно передать CancellationToken (например, из CancellationTokenSource)
            var file = await exportFunc(CancellationToken.None);

            await jS.InvokeVoidAsync(
                "downloadFile",
                fileName,
                contentType,
                file);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Export error ({fileName}): {ex.Message}");
        }
    }
}
