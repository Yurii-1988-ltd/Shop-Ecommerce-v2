using Ecommerce.Basket.Modules.Application.Responses;

namespace Ecommerce.Basket.Modules.Application.Mapping;

public static class BasketMapper
{
    public static BasketResponse ToResponse(this Domain.Entities.Basket basket)
    {
       
        return new BasketResponse(
            basket.Id,
            basket.CustomerId,
            basket.Items.Select(item => new BasketItemResponse(
                item.Id,
                item.ProductId,
                item.ProductName,
                item.UnitPrice.Amount,
                item.UnitPrice.Currency,
                item.Quantity.Value
            )).ToList(),
            basket.GrandTotal.Amount, // Итоговая сумма со скидками
            basket.Currency           // Валюта корзины
        );
    }
}