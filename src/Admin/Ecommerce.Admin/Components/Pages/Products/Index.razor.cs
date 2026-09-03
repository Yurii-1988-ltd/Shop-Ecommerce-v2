using Ecommerce.Admin.ApiClients.Catalog.Models;
using Ecommerce.Admin.Components.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Ecommerce.Admin.Components.Pages.Products;

public partial class Index : ComponentBase
{

    private string? _search;

    private MudTable<ProductListItemResponse>? _table;

    private async Task<TableData<ProductListItemResponse>> LoadProductsAsync(
        TableState state,
        CancellationToken cancellationToken)
    {
        var page = state.Page + 1;
        var pageSize = state.PageSize;

        var result = await CatalogApi.GetProductsAsync(
            page,
            pageSize,
            _search,
            cancellationToken);

        return new TableData<ProductListItemResponse>
        {
            Items = result?.Items ?? [],
            TotalItems = result?.TotalCount ?? 0
        };
    }

    private void Details(Guid id)
        => Navigation.NavigateTo($"/products/{id}");

    private void Edit(Guid id)
        => Navigation.NavigateTo($"/products/edit/{id}");

    private async Task Delete(ProductListItemResponse product)
    {
        var parameters = new DialogParameters
    {
        {
            nameof(ConfirmationDialog.Title),
            "Delete Product"
        },
        {
            nameof(ConfirmationDialog.Message),
            $"Are you sure you want to delete '{product.Name}'?\n\nThis action cannot be undone."
        }
    };

        var dialog = await DialogService.ShowAsync<ConfirmationDialog>(
            "Delete Product",
            parameters);

        var result = await dialog.Result;

        if (result.Canceled)
            return;

        await CatalogApi.DeleteAsync(product.Id);

        await Refresh();
    }

    private void OnSearchChanged(string? value)
    {
        _search = value;
        if (_table is null)
            return;

        _table?.NavigateTo(0);
    }

    private async Task Refresh()
    {
        if (_table is not null)
            await _table.ReloadServerData();
    }
}
