using ECommerceMVC.Domain.Entities;
using ECommerceMVC.Domain.Interfaces;
using ECommerceMVC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext _context;

    public ReviewRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Review?> GetByIdAsync(int id)
    {
        return await _context.Reviews
            .Include(r => r.Product)
            .Include(r => r.Customer)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Review>> GetAllAsync()
    {
        return await _context.Reviews
            .Include(r => r.Product)
            .Include(r => r.Customer)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetByProductIdAsync(int productId)
    {
        return await _context.Reviews
            .Include(r => r.Product)
            .Include(r => r.Customer)
            .Where(r => r.ProductId == productId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetByCustomerIdAsync(string customerId)
    {
        return await _context.Reviews
            .Include(r => r.Product)
            .Include(r => r.Customer)
            .Where(r => r.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task AddAsync(Review review)
    {
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Review review)
    {
        _context.Reviews.Update(review);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var review = await GetByIdAsync(id);
        if (review != null)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }
}
