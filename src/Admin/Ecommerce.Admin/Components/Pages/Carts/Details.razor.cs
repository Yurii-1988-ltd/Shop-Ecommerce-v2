

using Ecommerce.Admin.ApiClients.Carts.Contracts;
using Ecommerce.Admin.Components.Pages.Carts.Dialogs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Ecommerce.Admin.Components.Pages.Carts
{
    public partial class Details :ComponentBase
    {
        [Parameter]
        public Guid CustomerId { get; set; }

        private CartResponse? _cart;
        private string _couponCode = string.Empty;
        private bool _couponLoading;
        private bool _loading;
        private string? _couponError;

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            _loading = true;

            try
            {
                _cart = await CartApi.GetAsync(CustomerId);
            }
            finally
            {
                _loading = false;
            }
        }

        private async Task RemoveItem(Guid productId)
        {
            await CartApi.RemoveItemAsync(
                CustomerId,
                productId);

            await LoadAsync();
        }

        private async Task ClearCart()
        {
            await CartApi.ClearAsync(CustomerId);

            await LoadAsync();
        }
        private async Task ChangeQuantity(CartItemResponse item)
        {
            var parameters = new DialogParameters
    {
        {
            nameof(ChangeQuantityDialog.CurrentQuantity),
            item.Quantity
        }
    };

            var dialog = await DialogService.ShowAsync<ChangeQuantityDialog>(
                "Change Quantity",
                parameters);

            var result = await dialog.Result;

            if (result is null || result.Canceled)
                return;

            var quantity = (int)result.Data!;

            await CartApi.ChangeQuantityAsync(
                CustomerId,
                item.ProductId,
                new ChangeCartItemQuantityRequest(
                    CustomerId,
                    item.ProductId,
                    quantity));

            await LoadAsync();
        }
        private async Task ApplyCoupon()
        {
            if (string.IsNullOrWhiteSpace(_couponCode))
                return;
            _couponError = null;
            _couponLoading = true;

            try
            {
                await CartApi.ApplyCouponAsync(CustomerId, new ApplyCouponRequest(_couponCode));
                _couponCode = string.Empty;
                await LoadAsync();
            }
            catch(HttpRequestException ex)
            {
                _couponError = ex.Message;
            }
            finally
            {
                _couponLoading = false;

            }
        }
        private async Task RemoveCoupon()
        {
            _couponLoading = true;
            try
            {
                await CartApi.RemoveCouponAsync(CustomerId);
                await LoadAsync();


            }
            finally
            {
                _couponLoading = false;
            }
        }
    }
}
