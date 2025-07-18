using ECommerceMVC.Domain.Entities;

namespace ECommerceMVC.Domain.Interfaces;

public interface IReviewRepository
{
    Task<Review?> GetByIdAsync(int id);
    Task<IEnumerable<Review>> GetAllAsync();
    Task<IEnumerable<Review>> GetByProductIdAsync(int productId);
    Task<IEnumerable<Review>> GetByCustomerIdAsync(string customerId);
    Task AddAsync(Review review);
    Task UpdateAsync(Review review);
    Task DeleteAsync(int id);
}