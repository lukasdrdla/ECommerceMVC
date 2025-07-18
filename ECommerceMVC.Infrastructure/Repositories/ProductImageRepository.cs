using ECommerceMVC.Domain.Entities;
using ECommerceMVC.Domain.Interfaces;
using ECommerceMVC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Infrastructure.Repositories;

public class ProductImageRepository : IProductImageRepository
{
    private readonly ApplicationDbContext _context;

    public ProductImageRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProductImage?> GetByIdAsync(int id)
    {
        return await _context.ProductImages
            .Include(pi => pi.Product)
            .FirstOrDefaultAsync(pi => pi.Id == id);
    }

    public async Task<IEnumerable<ProductImage>> GetAllAsync()
    {
        return await _context.ProductImages
            .Include(pi => pi.Product)
            .ToListAsync();
    }

    public async Task AddAsync(ProductImage productImage)
    {
        await _context.ProductImages.AddAsync(productImage);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProductImage productImage)
    {
        _context.ProductImages.Update(productImage);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var productImage = await GetByIdAsync(id);
        if (productImage != null)
        {
            _context.ProductImages.Remove(productImage);
            await _context.SaveChangesAsync();
        }
    }
}
