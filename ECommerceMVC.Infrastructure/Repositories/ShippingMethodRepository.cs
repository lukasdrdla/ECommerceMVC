using ECommerceMVC.Domain.Entities;
using ECommerceMVC.Domain.Interfaces;
using ECommerceMVC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Infrastructure.Repositories;

public class ShippingMethodRepository : IShippingMethodRepository
{
    private readonly ApplicationDbContext _context;

    public ShippingMethodRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ShippingMethod?> GetByIdAsync(int id)
    {
        return await _context.ShippingMethods.FindAsync(id);
    }

    public async Task<IEnumerable<ShippingMethod>> GetAllAsync()
    {
        return await _context.ShippingMethods.ToListAsync();
    }

    public async Task AddAsync(ShippingMethod shippingMethod)
    {
        await _context.ShippingMethods.AddAsync(shippingMethod);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ShippingMethod shippingMethod)
    {
        _context.ShippingMethods.Update(shippingMethod);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var shippingMethod = await GetByIdAsync(id);
        if (shippingMethod != null)
        {
            _context.ShippingMethods.Remove(shippingMethod);
            await _context.SaveChangesAsync();
        }
    }
}
