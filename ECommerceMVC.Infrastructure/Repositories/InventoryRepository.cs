using ECommerceMVC.Domain.Entities;
using ECommerceMVC.Domain.Interfaces;
using ECommerceMVC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Infrastructure.Repositories;

public class InventoryRepository : IInventoryRepository
{
    private readonly ApplicationDbContext _context;

    public InventoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Inventory?> GetByIdAsync(int id)
    {
        return await _context.Inventories
            .Include(i => i.ProductVariant)
            .Include(i => i.Warehouse)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Inventory>> GetAllAsync()
    {
        return await _context.Inventories
            .Include(i => i.ProductVariant)
            .Include(i => i.Warehouse)
            .ToListAsync();
    }

    public async Task AddAsync(Inventory inventory)
    {
        await _context.Inventories.AddAsync(inventory);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Inventory inventory)
    {
        _context.Inventories.Update(inventory);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var inventory = await GetByIdAsync(id);
        if (inventory != null)
        {
            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Inventory>> GetByWarehouseIdAsync(int warehouseId)
    {
        return await _context.Inventories
            .Include(i => i.ProductVariant)
            .Include(i => i.Warehouse)
            .Where(i => i.WarehouseId == warehouseId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Inventory>> GetByProductVariantIdAsync(int productVariantId)
    {
        return await _context.Inventories
            .Include(i => i.ProductVariant)
            .Include(i => i.Warehouse)
            .Where(i => i.ProductVariantId == productVariantId)
            .ToListAsync();
    }

    public async Task<Inventory?> GetByProductVariantAndWarehouseAsync(int productVariantId, int warehouseId)
    {
        return await _context.Inventories
            .Include(i => i.ProductVariant)
            .Include(i => i.Warehouse)
            .FirstOrDefaultAsync(i => i.ProductVariantId == productVariantId && i.WarehouseId == warehouseId);
    }

    public async Task<IEnumerable<Warehouse>> GetWarehousesWithProductVariantAsync(int productVariantId)
    {
        return await _context.Inventories
            .Include(i => i.Warehouse)
            .Where(i => i.ProductVariantId == productVariantId)
            .Select(i => i.Warehouse)
            .Distinct()
            .ToListAsync();
    }
}
