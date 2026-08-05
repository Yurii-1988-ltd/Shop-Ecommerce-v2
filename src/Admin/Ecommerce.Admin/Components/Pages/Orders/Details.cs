using Ecommerce.Admin.ApiClients.Orders.Enums;
using Ecommerce.Admin.ApiClients.Orders.Models;
using Ecommerce.Admin.Components.Pages.Orders.Dialogs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Ecommerce.Admin.Components.Pages.Orders;

public partial class Details
{
    [Parameter] public Guid Id { get; set; }

    private OrderResponse? _order;


    [Parameter]
    public IReadOnlyCollection<OrderItemResponse> Items { get; set; } = [];

    [Parameter]
    public EventCallback OnAddItem { get; set; }

    [Parameter]
    public EventCallback<Guid> OnRemoveItem { get; set; }

    [Parameter]
    public EventCallback<(Guid ItemId, int Quantity)> OnChangeQuantity { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await LoadOrderAsync();
    }

    private async Task LoadOrderAsync()
    {
        _order = await OrderApi.GetAsync(Id);

        if (_order is null)
        {
            Navigation.NavigateTo("/orders");
        }
    }

    private async Task OpenSubmitDialogAsync()
    {
        if (_order is null) return;

        var parameters = new DialogParameters<SubmitOrderDialog>
        {
            { x => x.OrderNumber, _order.OrderNumber }
        };

        var dialog = await DialogService.ShowAsync<SubmitOrderDialog>("Submit order", parameters);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            await SubmitOrderAsync();
        }
    }

    private async Task SubmitOrderAsync()
    {
        if (_order is null) return;

        try
        {
            await OrderApi.SubmitAsync(_order.Id);
            Snackbar.Add("Order submitted successfully.", Severity.Success);
            await LoadOrderAsync();
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    private async Task OpenChangeStatusDialogAsync()
    {
        if (_order is null) return;

        var parameters = new DialogParameters<ChangeOrderStatusDialog>
        {
            { x => x.OrderNumber, _order.OrderNumber },
            { x => x.CurrentStatus, _order.Status }
        };

        var options = new DialogOptions
        {
            CloseButton = true,
            MaxWidth = MaxWidth.ExtraSmall,
            FullWidth = true
        };

        var dialog = await DialogService.ShowAsync<ChangeOrderStatusDialog>("Change Status", parameters, options);
        var result = await dialog.Result;

        if (!result.Canceled && result.Data is OrderStatus newStatus)
        {
            await ChangeStatusAsync(newStatus);
        }
    }

    private async Task ChangeStatusAsync(OrderStatus newStatus)
    {
        if (_order is null) return;

        try
        {
            await OrderApi.ChangeStatusAsync(_order.Id, new ChangeStatusRequest(newStatus));
            Snackbar.Add($"Status updated to {newStatus}.", Severity.Success);
            await LoadOrderAsync();
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    private async Task OpenForceChangeStatusDialogAsync()
    {
        if (_order is null) return;

        var parameters = new DialogParameters<ForceChangeStatusDialog>
        {
            { x => x.OrderNumber, _order.OrderNumber },
            { x => x.CurrentStatus, _order.Status }
        };

        var options = new DialogOptions
        {
            CloseButton = true,
            MaxWidth = MaxWidth.Small,
            FullWidth = true
        };

        var dialog = await DialogService.ShowAsync<ForceChangeStatusDialog>(
            "Force Change Status",
            parameters,
            options);

        var result = await dialog.Result;

        if (result.Canceled) return;

        if (result.Data is ForceChangeStatusDialog.ForceChangeStatusResult data)
        {
            await ForceChangeStatusAsync(data);
        }
    }

    private async Task ForceChangeStatusAsync(ForceChangeStatusDialog.ForceChangeStatusResult data)
    {
        if (_order is null) return;

        try
        {
            await OrderApi.ForceChangeStatusAsync(
                _order.Id,
                new ForceChangeStatusRequest(
                    data.Status,
                    data.Reason));

            Snackbar.Add("Order status force-changed successfully.", Severity.Success);
            await LoadOrderAsync();
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
    private async Task OpenAddItemDialogAsync()
    {
        var products = await CatalogApi.GetProductsAsync(
            page: 1,
            pageSize: 100);

        var parameters = new DialogParameters
{
    { nameof(OrderAddItemDialog.Products), products?.Items }
};

        var dialog = await DialogService.ShowAsync<OrderAddItemDialog>(
            "Add Product",
            parameters);

        var result = await dialog.Result;

        if (result.Canceled)
            return;

        if (result.Data is not OrderAddItemDialogResult data)
            return;

        await OrderApi.AddOrderItemAsync(
            _order!.Id,
            new AddOrderItemRequest(
                data.ProductId,
                data.Quantity));

        Snackbar.Add("Product added.", Severity.Success);

        await LoadOrderAsync();
    }

}