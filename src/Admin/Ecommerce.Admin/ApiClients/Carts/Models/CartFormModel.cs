using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Admin.ApiClients.Carts.Models;

public sealed class CartFormModel
{
    [Required]
    public string CustomerId { get; set; } = string.Empty; 
}