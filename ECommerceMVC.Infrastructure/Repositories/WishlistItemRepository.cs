using ECommerceMVC.Domain.Entities;
using ECommerceMVC.Domain.Interfaces;
using ECommerceMVC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Infrastructure.Repositories;

public class WishlistItemRepository : IWishlistItemRepository
{
    private readonly ApplicationDbContext _context;

    public WishlistItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<WishlistItem?> GetByIdAsync(int id)
    {
        return await _context.WishlistItems
            .Include(wi => wi.Wishlist)
            .Include(wi => wi.Product)
            .ThenInclude(p => p.Brand)
            .Include(wi => wi.Product)
            .ThenInclude(p => p.Category)
            .FirstOrDefaultAsync(wi => wi.Id == id);
    }

    public async Task<IEnumerable<WishlistItem>> GetAllAsync()
    {
        return await _context.WishlistItems
            .Include(wi => wi.Wishlist)
            .Include(wi => wi.Product)
            .ToListAsync();
    }

    public async Task AddAsync(WishlistItem wishlistItem)
    {
        await _context.WishlistItems.AddAsync(wishlistItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(WishlistItem wishlistItem)
    {
        _context.WishlistItems.Update(wishlistItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var wishlistItem = await GetByIdAsync(id);
        if (wishlistItem != null)
        {
            _context.WishlistItems.Remove(wishlistItem);
            await _context.SaveChangesAsync();
        }
    }
}
