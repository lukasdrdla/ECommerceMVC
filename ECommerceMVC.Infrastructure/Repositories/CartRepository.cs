using ECommerceMVC.Domain.Entities;
using ECommerceMVC.Domain.Interfaces;
using ECommerceMVC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Infrastructure.Repositories;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _context;

    public CartRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Cart?> GetByIdAsync(int id)
    {
        return await _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Cart>> GetAllAsync()
    {
        return await _context.Carts
            .Include(c => c.CartItems)
            .ToListAsync();
    }

    public async Task AddAsync(Cart cart)
    {
        await _context.Carts.AddAsync(cart);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Cart cart)
    {
        _context.Carts.Update(cart);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var cart = await GetByIdAsync(id);
        if (cart != null)
        {
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
        }
    }
}
