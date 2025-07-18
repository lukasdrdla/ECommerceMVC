using ECommerceMVC.Domain.Entities;

namespace ECommerceMVC.Domain.Interfaces;

public interface IWarehouseRepository
{
    Task<Warehouse?> GetByIdAsync(int id);
    Task<IEnumerable<Warehouse>> GetAllAsync();
    Task AddAsync(Warehouse warehouse);
    Task UpdateAsync(Warehouse warehouse);
    Task DeleteAsync(int id);
}