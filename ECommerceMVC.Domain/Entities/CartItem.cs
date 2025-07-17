namespace ECommerceMVC.Domain.Entities;

public class CartItem
{
    public int Id { get; set; }
    public int Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; } = 0.0m;

    public int CartId { get; set; }
    public Cart Cart { get; set; } = new Cart();
    public int ProductId { get; set; }
    public Product Product { get; set; } = new Product();
}