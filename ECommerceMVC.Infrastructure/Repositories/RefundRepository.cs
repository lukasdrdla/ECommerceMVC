using ECommerceMVC.Domain.Entities;
using ECommerceMVC.Domain.Interfaces;
using ECommerceMVC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Infrastructure.Repositories;

public class RefundRepository : IRefundRepository
{
    private readonly ApplicationDbContext _context;

    public RefundRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RefundRequest?> GetByIdAsync(int id)
    {
        return await _context.RefundRequests
            .Include(rr => rr.OrderItem)
            .FirstOrDefaultAsync(rr => rr.Id == id);
    }

    public async Task<IEnumerable<RefundRequest>> GetAllAsync()
    {
        return await _context.RefundRequests
            .Include(rr => rr.OrderItem)
            .ToListAsync();
    }

    public async Task AddAsync(RefundRequest refundRequest)
    {
        await _context.RefundRequests.AddAsync(refundRequest);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(RefundRequest refundRequest)
    {
        _context.RefundRequests.Update(refundRequest);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var refundRequest = await GetByIdAsync(id);
        if (refundRequest != null)
        {
            _context.RefundRequests.Remove(refundRequest);
            await _context.SaveChangesAsync();
        }
    }
}
