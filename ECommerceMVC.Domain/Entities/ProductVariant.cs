using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceMVC.Domain.Entities
{
    public class ProductVariant
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string Sku { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int DiscountPercentage { get; set; } = 0;
        public decimal DiscountedPrice => Price - Price * DiscountPercentage / 100;

        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsOnSale => DiscountPercentage > 0;
        public ICollection<ProductVariantAttribute> Attributes { get; set; } = new List<ProductVariantAttribute>();
        public ICollection<ProductVariantImage> ProductVariantImages { get; set; } = new List<ProductVariantImage>();

    }

}
