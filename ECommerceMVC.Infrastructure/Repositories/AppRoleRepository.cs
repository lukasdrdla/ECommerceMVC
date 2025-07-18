using ECommerceMVC.Domain.Entities;
using ECommerceMVC.Domain.Interfaces;
using ECommerceMVC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Infrastructure.Repositories;

public class AppRoleRepository : IAppRoleRepository
{
    private readonly ApplicationDbContext _context;

    public AppRoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AppRole?> GetByIdAsync(int id)
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r.Id == id.ToString());
    }

    public async Task<IEnumerable<AppRole>> GetAllAsync()
    {
        return await _context.Roles.ToListAsync();
    }

    public async Task AddAsync(AppRole appRole)
    {
        await _context.Roles.AddAsync(appRole);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(AppRole appRole)
    {
        _context.Roles.Update(appRole);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var appRole = await GetByIdAsync(id);
        if (appRole != null)
        {
            _context.Roles.Remove(appRole);
            await _context.SaveChangesAsync();
        }
    }
}
