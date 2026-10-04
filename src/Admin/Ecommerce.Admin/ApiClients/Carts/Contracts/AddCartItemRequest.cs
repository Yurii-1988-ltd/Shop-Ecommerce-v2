
    namespace Ecommerce.Admin.ApiClients.Carts.Contracts;

    public sealed record AddCartItemRequest(
    
         Guid ProductId ,
         string Name ,
         string Sku,
        decimal Price,
       string Currency ,
        int  Quantity 
    );

        

