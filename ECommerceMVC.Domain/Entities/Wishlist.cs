namespace ECommerceMVC.Domain.Entities;

public class Wishlist
{
    public int Id { get; set; }

    public string AppUserId { get; set; } = string.Empty;
    public AppUser AppUser { get; set; } = new AppUser();

    public ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
}