using Ecommerce.Shared.Contracts.Orders;
using Ecommerce.Storefront.ApiClients.Carts.Models;
using Microsoft.AspNetCore.Components;

namespace Ecommerce.Storefront.Components.Pages.Checkout;

public partial class Checkout
{
    private CartResponse? _cart;

    private bool _isLoading = true;

    private string _email = string.Empty;
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;
    private string _address = string.Empty;
    private string _city = string.Empty;
    private string _postalCode = string.Empty;
    private string _country = "Ukraine";
    private string _phone = string.Empty;
    private bool _isSubmitting;
    private string? _errorMessage;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _cart = await CartApiClient.GetAsync();
        }
        finally
        {
            _isLoading = false;
        }
    }

    private void OnEmailChanged(ChangeEventArgs e)
    {
        _email = e.Value?.ToString() ?? string.Empty;
    }

    private async Task PlaceOrder()
    {
        if (_isSubmitting)
        {
            return;
        }

        _errorMessage = null;

        try
        {
            if (_cart is null || !_cart.Items.Any())
            {
                _errorMessage = "Cart is empty.";
                return;
            }

            if (string.IsNullOrWhiteSpace(_email))
            {
                _errorMessage = "Please enter your email address.";
                return;
            }

            _isSubmitting = true;

            var items = _cart.Items
                .Select(item => new CreateOrderItemDto(
                    item.ProductId,
                    item.Name,
                    item.Sku,
                    item.Price,
                    item.Quantity))
                .ToList();

            var request = new CreateOrderRequest(
                CustomerId: CurrentUser.IsAuthenticated ? CurrentUser.UserId : null,
                CustomerEmail: _email,
                ShippingAddress: new OrderAddressDto(
                    _firstName,
                    _lastName,
                    _country,
                    _city,
                    _address,
                    _postalCode),
                Items: items,
                Currency: _cart.Currency);

            _errorMessage = "Creating order...";

            var result = await OrdersApiClient.CreateAsync(request, cancellationToken: default);

            // Очистка корзины в зависимости от статуса авторизации
            if (CurrentUser.IsAuthenticated)
            {
                await CartApiClient.ClearAsync();
            }
            else
            {
                await CartApiClient.ClearGuestCartAsync();
            }

            Navigation.NavigateTo($"/order-success/{result.OrderId}?orderNumber={Uri.EscapeDataString(result.OrderNumber)}");
        }
        catch (Exception ex)
        {
            _errorMessage = $"{ex.GetType().Name}: {ex.Message}";
        }
        finally
        {
            _isSubmitting = false;
        }
    }
}