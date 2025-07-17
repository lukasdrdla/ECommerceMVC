using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceMVC.Domain.Entities
{
    public class ShippingMethod
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty; // Např. "Zásilkovna", "GLS"
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public int EstimatedDeliveryDays { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
