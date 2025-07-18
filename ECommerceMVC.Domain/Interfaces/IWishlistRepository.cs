using ECommerceMVC.Domain.Entities;

namespace ECommerceMVC.Domain.Interfaces;

public interface IWishlistRepository
{
    Task<Wishlist?> GetByIdAsync(int id);
    Task<IEnumerable<Wishlist>> GetAllAsync();
    Task AddAsync(Wishlist wishlist);
    Task UpdateAsync(Wishlist wishlist);
    Task DeleteAsync(int id);
}
