using ECommerceMVC.Domain.Entities;
using ECommerceMVC.Domain.Interfaces;
using ECommerceMVC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Infrastructure.Repositories;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly ApplicationDbContext _context;

    public OrderItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OrderItem?> GetByIdAsync(int id)
    {
        return await _context.OrderItems
            .Include(oi => oi.Order)
            .Include(oi => oi.ProductVariant)
            .ThenInclude(pv => pv.Product)
            .FirstOrDefaultAsync(oi => oi.Id == id);
    }

    public async Task<IEnumerable<OrderItem>> GetAllAsync()
    {
        return await _context.OrderItems
            .Include(oi => oi.Order)
            .Include(oi => oi.ProductVariant)
            .ToListAsync();
    }

    public async Task AddAsync(OrderItem orderItem)
    {
        await _context.OrderItems.AddAsync(orderItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(OrderItem orderItem)
    {
        _context.OrderItems.Update(orderItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var orderItem = await GetByIdAsync(id);
        if (orderItem != null)
        {
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
        }
    }
}
