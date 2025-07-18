using ECommerceMVC.Domain.Entities;

namespace ECommerceMVC.Domain.Interfaces;

public interface IAppUserRepository
{
    Task<AppUser?> GetByIdAsync(int id);
    Task<IEnumerable<AppUser>> GetAllAsync();
    Task AddAsync(AppUser appUser);
    Task UpdateAsync(AppUser appUser);
    Task DeleteAsync(int id);
}
