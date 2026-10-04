namespace Ecommerce.Storefront.ApiClients.Carts.Models;

public sealed class CartState
{
    public int TotalItems { get;private set; }
    public event Action? Changed;

    public void SetTotalItems(int totalItems)
    {
        TotalItems = totalItems;
        Changed?.Invoke();
    }
}
