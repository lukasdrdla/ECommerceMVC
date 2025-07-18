using ECommerceMVC.Domain.Entities;

namespace ECommerceMVC.Domain.Interfaces;

public interface IRefundRepository
{
    Task<RefundRequest?> GetByIdAsync(int id);
    Task<IEnumerable<RefundRequest>> GetAllAsync();
    Task AddAsync(RefundRequest refundRequest);
    Task UpdateAsync(RefundRequest refundRequest);
    Task DeleteAsync(int id);
}