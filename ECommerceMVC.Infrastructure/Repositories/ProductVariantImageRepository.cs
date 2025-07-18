using ECommerceMVC.Domain.Entities;
using ECommerceMVC.Domain.Interfaces;
using ECommerceMVC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Infrastructure.Repositories;

public class ProductVariantImageRepository : IProductVariantImageRepository
{
    private readonly ApplicationDbContext _context;

    public ProductVariantImageRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProductVariantImage?> GetByIdAsync(int id)
    {
        return await _context.ProductVariantImages
            .Include(pvi => pvi.ProductVariant)
            .ThenInclude(pv => pv.Product)
            .FirstOrDefaultAsync(pvi => pvi.Id == id);
    }

    public async Task<IEnumerable<ProductVariantImage>> GetAllAsync()
    {
        return await _context.ProductVariantImages
            .Include(pvi => pvi.ProductVariant)
            .ToListAsync();
    }

    public async Task AddAsync(ProductVariantImage productVariantImage)
    {
        await _context.ProductVariantImages.AddAsync(productVariantImage);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProductVariantImage productVariantImage)
    {
        _context.ProductVariantImages.Update(productVariantImage);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var productVariantImage = await GetByIdAsync(id);
        if (productVariantImage != null)
        {
            _context.ProductVariantImages.Remove(productVariantImage);
            await _context.SaveChangesAsync();
        }
    }
}
