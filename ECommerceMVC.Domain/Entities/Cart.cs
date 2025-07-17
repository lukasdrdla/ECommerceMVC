namespace ECommerceMVC.Domain.Entities;

public class Cart
{
    public int Id { get; set; }
    public string CustomerId { get; set; } = string.Empty;
    public AppUser Customer { get; set; } = new AppUser();

    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}