using Ecommerce.Admin.ApiClients.Catalog.Models;
using Ecommerce.Admin.ApiClients.Inventories.Contracts;
using Ecommerce.Admin.Contracts;
using MudBlazor;
namespace Ecommerce.Admin.Components.Pages.Inventories;

public partial class Create
{
    private MudForm? _form;

    private ProductListItemResponse? _selectedProduct;

    private int _quantity;
    private int _minimumQuantity;

    private bool _saving;

    private async Task<IEnumerable<ProductListItemResponse>> SearchProducts(
        string? value,
        CancellationToken cancellationToken)
    {
        var search = string.IsNullOrWhiteSpace(value)
            ? null
            : value;

        var result = await CatalogApi.GetProductsAsync(
            page: 1,
            pageSize: 20,
            search: search,
            cancellationToken);

        return result?.Items ?? [];
    }

    private void OnProductChanged(ProductListItemResponse? product)
    {
        _selectedProduct = product;
    }

    private async Task CreateInventory()
    {
        if (_form is null)
            return;

        await _form.Validate();

        if (!_form.IsValid || _selectedProduct is null)
            return;

        _saving = true;

        try
        {
            var request = new CreateInventoryItemRequest(
                _selectedProduct.Id,
                _selectedProduct.Sku,
                _quantity,
                _minimumQuantity);

            await InventoryApi.CreateAsync(request);

            Snackbar.Add(
                "Inventory created successfully.",
                Severity.Success);

            Navigation.NavigateTo("/inventories");
        }
        catch (ApiException ex) when (ex.Code == "Inventory.AlreadyExists")
        {
            Snackbar.Add(
                "Inventory for this product already exists.",
                Severity.Warning);
        }
        catch (ApiException ex) when (ex.Code == "Inventory.AlreadyExists")
        {
            Snackbar.Add(
                $"Inventory for product '{_selectedProduct?.Sku}' already exists.",
                Severity.Warning);
        }
        catch (HttpRequestException)
        {
            Snackbar.Add(
                "Failed to create inventory.",
                Severity.Error);
        }
        finally
        {
            _saving = false;
        }
    }

    private void Cancel()
    {
        Navigation.NavigateTo("/inventories");
    }
}
