using Ecommerce.Shared.Contracts.Orders;
using Ecommerce.Storefront.ApiClients.Carts.Models;
using Microsoft.AspNetCore.Components;

namespace Ecommerce.Storefront.Components.Pages.Checkout
{
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

                // TODO:
                // When the current user profile endpoint is ready,
                // load customer information here.
                //
                // _email = currentUser.Email ?? string.Empty;
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
            _errorMessage = null;

            try
            {
                _errorMessage = $"Authenticated: {CurrentUser.IsAuthenticated}, UserId: {CurrentUser.UserId}";

                if (!CurrentUser.IsAuthenticated)
                {
                    Navigation.NavigateTo("/auth/login?returnUrl=/checkout");
                    return;
                }

                if (_cart is null || !_cart.Items.Any())
                {
                    _errorMessage = "Cart is empty.";
                    return;
                }

                if (CurrentUser.UserId == Guid.Empty)
                {
                    _errorMessage = "UserId is empty.";
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
                    CustomerId: CurrentUser.UserId,
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
                await CartApiClient.ClearAsync();

                _errorMessage = $"Order created: {result.OrderId}";

                Navigation.NavigateTo(
                             $"/order-success/{result.OrderId}?orderNumber={Uri.EscapeDataString(result.OrderNumber)}");
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
}
