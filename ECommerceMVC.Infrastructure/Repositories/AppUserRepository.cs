using ECommerceMVC.Domain.Entities;
using ECommerceMVC.Domain.Interfaces;
using ECommerceMVC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Infrastructure.Repositories;

public class AppUserRepository : IAppUserRepository
{
    private readonly ApplicationDbContext _context;

    public AppUserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AppUser?> GetByIdAsync(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id.ToString());
    }

    public async Task<IEnumerable<AppUser>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task AddAsync(AppUser appUser)
    {
        await _context.Users.AddAsync(appUser);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(AppUser appUser)
    {
        _context.Users.Update(appUser);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var appUser = await GetByIdAsync(id);
        if (appUser != null)
        {
            _context.Users.Remove(appUser);
            await _context.SaveChangesAsync();
        }
    }
}
