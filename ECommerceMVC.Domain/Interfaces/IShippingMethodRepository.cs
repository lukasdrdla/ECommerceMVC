using ECommerceMVC.Domain.Entities;

namespace ECommerceMVC.Domain.Interfaces;

public interface IShippingMethodRepository
{
    Task<ShippingMethod?> GetByIdAsync(int id);
    Task<IEnumerable<ShippingMethod>> GetAllAsync();
    Task AddAsync(ShippingMethod shippingMethod);
    Task UpdateAsync(ShippingMethod shippingMethod);
    Task DeleteAsync(int id);
}