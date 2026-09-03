using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Admin.ApiClients.Catalog.Models;

public sealed class ProductFormModel
{

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 3)]
        
        public string Sku { get; set; } = string.Empty;
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string Currency { get; set; } = "USD";
    
}