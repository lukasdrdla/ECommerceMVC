using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceMVC.Domain.Entities
{
    public class ProductVariantAttribute
    {
        public int Id { get; set; }

        public int ProductVariantId { get; set; }
        public ProductVariant ProductVariant { get; set; } = new ProductVariant();

        public string Name { get; set; } = string.Empty;   // např. "Barva", "Kapacita"
        public string Value { get; set; } = string.Empty;  // např. "Černá", "128 GB"
    }
}
