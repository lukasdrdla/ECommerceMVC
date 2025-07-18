using ECommerceMVC.Domain.Entities;
using ECommerceMVC.Domain.Interfaces;
using ECommerceMVC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Infrastructure.Repositories;

public class ProductVariantRepository : IProductVariantRepository
{
    private readonly ApplicationDbContext _context;

    public ProductVariantRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProductVariant?> GetByIdAsync(int id)
    {
        return await _context.ProductVariants
            .Include(pv => pv.Product)
            .Include(pv => pv.ProductVariantImages)
            .FirstOrDefaultAsync(pv => pv.Id == id);
    }

    public async Task<IEnumerable<ProductVariant>> GetAllAsync()
    {
        return await _context.ProductVariants
            .Include(pv => pv.Product)
            .Include(pv => pv.ProductVariantImages)
            .ToListAsync();
    }

    public async Task AddAsync(ProductVariant productVariant)
    {
        await _context.ProductVariants.AddAsync(productVariant);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProductVariant productVariant)
    {
        _context.ProductVariants.Update(productVariant);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var productVariant = await GetByIdAsync(id);
        if (productVariant != null)
        {
            _context.ProductVariants.Remove(productVariant);
            await _context.SaveChangesAsync();
        }
    }
}
