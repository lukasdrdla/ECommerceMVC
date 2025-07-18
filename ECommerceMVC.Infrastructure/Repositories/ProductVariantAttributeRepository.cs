using ECommerceMVC.Domain.Entities;
using ECommerceMVC.Domain.Interfaces;
using ECommerceMVC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Infrastructure.Repositories;

public class ProductVariantAttributeRepository : IProductVariantAttributeRepository
{
    private readonly ApplicationDbContext _context;

    public ProductVariantAttributeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProductVariantAttribute?> GetByIdAsync(int id)
    {
        return await _context.ProductVariantAttributes
            .Include(pva => pva.ProductVariant)
            .ThenInclude(pv => pv.Product)
            .FirstOrDefaultAsync(pva => pva.Id == id);
    }

    public async Task<IEnumerable<ProductVariantAttribute>> GetAllAsync()
    {
        return await _context.ProductVariantAttributes
            .Include(pva => pva.ProductVariant)
            .ToListAsync();
    }

    public async Task AddAsync(ProductVariantAttribute productVariantAttribute)
    {
        await _context.ProductVariantAttributes.AddAsync(productVariantAttribute);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProductVariantAttribute productVariantAttribute)
    {
        _context.ProductVariantAttributes.Update(productVariantAttribute);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var productVariantAttribute = await GetByIdAsync(id);
        if (productVariantAttribute != null)
        {
            _context.ProductVariantAttributes.Remove(productVariantAttribute);
            await _context.SaveChangesAsync();
        }
    }
}
