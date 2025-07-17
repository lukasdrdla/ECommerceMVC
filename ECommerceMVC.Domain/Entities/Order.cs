namespace ECommerceMVC.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public AppUser Customer { get; set; } = new AppUser();
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public int ShippingMethodId { get; set; }
        public ShippingMethod ShippingMethod { get; set; } = new ShippingMethod();

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}