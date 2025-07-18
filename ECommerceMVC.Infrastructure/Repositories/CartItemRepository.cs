using ECommerceMVC.Domain.Entities;
using ECommerceMVC.Domain.Interfaces;
using ECommerceMVC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Infrastructure.Repositories;

public class CartItemRepository : ICartItemRepository
{
    private readonly ApplicationDbContext _context;

    public CartItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CartItem?> GetByIdAsync(int id)
    {
        return await _context.CartItems
            .Include(ci => ci.Product)
            .FirstOrDefaultAsync(ci => ci.Id == id);
    }

    public async Task<IEnumerable<CartItem>> GetAllAsync()
    {
        return await _context.CartItems
            .Include(ci => ci.Product)
            .ToListAsync();
    }

    public async Task AddAsync(CartItem cartItem)
    {
        await _context.CartItems.AddAsync(cartItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CartItem cartItem)
    {
        _context.CartItems.Update(cartItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var cartItem = await GetByIdAsync(id);
        if (cartItem != null)
        {
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }
    }
}
