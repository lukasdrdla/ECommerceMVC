using ECommerceMVC.Domain.Entities;
using ECommerceMVC.Domain.Interfaces;
using ECommerceMVC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Infrastructure.Repositories;

public class ProductTagRepository : IProductTagRepository
{
    private readonly ApplicationDbContext _context;

    public ProductTagRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductTag>> GetByProductIdAsync(int productId)
    {
        return await _context.ProductTags
            .Include(pt => pt.Product)
            .Include(pt => pt.Tag)
            .Where(pt => pt.ProductId == productId)
            .ToListAsync();
    }

    public async Task AddAsync(ProductTag productTag)
    {
        await _context.ProductTags.AddAsync(productTag);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int productId, int tagId)
    {
        var productTag = await _context.ProductTags
            .FirstOrDefaultAsync(pt => pt.ProductId == productId && pt.TagId == tagId);
        
        if (productTag != null)
        {
            _context.ProductTags.Remove(productTag);
            await _context.SaveChangesAsync();
        }
    }
}
