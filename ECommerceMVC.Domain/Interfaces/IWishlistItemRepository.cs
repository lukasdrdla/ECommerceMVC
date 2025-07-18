using ECommerceMVC.Domain.Entities;

namespace ECommerceMVC.Domain.Interfaces;

public interface IWishlistItemRepository
{
    Task<WishlistItem?> GetByIdAsync(int id);
    Task<IEnumerable<WishlistItem>> GetAllAsync();
    Task AddAsync(WishlistItem wishlistItem);
    Task UpdateAsync(WishlistItem wishlistItem);
    Task DeleteAsync(int id);
}
