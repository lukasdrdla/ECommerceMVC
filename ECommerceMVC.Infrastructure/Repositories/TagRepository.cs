using ECommerceMVC.Domain.Entities;
using ECommerceMVC.Domain.Interfaces;
using ECommerceMVC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Infrastructure.Repositories;

public class TagRepository : ITagRepository
{
    private readonly ApplicationDbContext _context;

    public TagRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Tag?> GetByIdAsync(int id)
    {
        return await _context.Tags
            .Include(t => t.ProductTags)
            .ThenInclude(pt => pt.Product)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Tag>> GetAllAsync()
    {
        return await _context.Tags.ToListAsync();
    }

    public async Task AddAsync(Tag tag)
    {
        await _context.Tags.AddAsync(tag);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Tag tag)
    {
        _context.Tags.Update(tag);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var tag = await GetByIdAsync(id);
        if (tag != null)
        {
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
        }
    }
}
