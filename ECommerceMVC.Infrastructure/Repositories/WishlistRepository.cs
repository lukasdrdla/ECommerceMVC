using ECommerceMVC.Domain.Entities;
using ECommerceMVC.Domain.Interfaces;
using ECommerceMVC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Infrastructure.Repositories;

public class WishlistRepository : IWishlistRepository
{
    private readonly ApplicationDbContext _context;

    public WishlistRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Wishlist?> GetByIdAsync(int id)
    {
        return await _context.Wishlists
            .Include(w => w.WishlistItems)
            .ThenInclude(wi => wi.Product)
            .Include(w => w.AppUser)
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<IEnumerable<Wishlist>> GetAllAsync()
    {
        return await _context.Wishlists
            .Include(w => w.WishlistItems)
            .Include(w => w.AppUser)
            .ToListAsync();
    }

    public async Task AddAsync(Wishlist wishlist)
    {
        await _context.Wishlists.AddAsync(wishlist);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Wishlist wishlist)
    {
        _context.Wishlists.Update(wishlist);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var wishlist = await GetByIdAsync(id);
        if (wishlist != null)
        {
            _context.Wishlists.Remove(wishlist);
            await _context.SaveChangesAsync();
        }
    }
}
