using ECommerceMVC.Domain.Entities;

namespace ECommerceMVC.Domain.Interfaces;

public interface IAppRoleRepository
{
    Task<AppRole?> GetByIdAsync(int id);
    Task<IEnumerable<AppRole>> GetAllAsync();
    Task AddAsync(AppRole appRole);
    Task UpdateAsync(AppRole appRole);
    Task DeleteAsync(int id);
}
