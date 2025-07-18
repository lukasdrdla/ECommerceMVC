using ECommerceMVC.Domain.Entities;
using ECommerceMVC.Domain.Interfaces;
using ECommerceMVC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Infrastructure.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly ApplicationDbContext _context;

    public WarehouseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Warehouse?> GetByIdAsync(int id)
    {
        return await _context.Warehouses
            .Include(w => w.Inventories)
            .ThenInclude(i => i.ProductVariant)
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<IEnumerable<Warehouse>> GetAllAsync()
    {
        return await _context.Warehouses.ToListAsync();
    }

    public async Task AddAsync(Warehouse warehouse)
    {
        await _context.Warehouses.AddAsync(warehouse);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Warehouse warehouse)
    {
        _context.Warehouses.Update(warehouse);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var warehouse = await GetByIdAsync(id);
        if (warehouse != null)
        {
            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();
        }
    }
}
