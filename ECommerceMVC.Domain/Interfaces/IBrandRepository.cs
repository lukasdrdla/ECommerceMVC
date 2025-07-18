using ECommerceMVC.Domain.Entities;

namespace ECommerceMVC.Domain.Interfaces;

public interface IBrandRepository
{
    Task<Brand?> GetByIdAsync(int id);
    Task<IEnumerable<Brand>> GetAllAsync();
    Task AddAsync(Brand brand);
    Task UpdateAsync(Brand brand);
    Task DeleteAsync(int id);
}