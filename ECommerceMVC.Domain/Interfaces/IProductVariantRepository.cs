using ECommerceMVC.Domain.Entities;

namespace ECommerceMVC.Domain.Interfaces;

public interface IProductVariantRepository
{
    Task<ProductVariant?> GetByIdAsync(int id);
    Task<IEnumerable<ProductVariant>> GetAllAsync();
    Task AddAsync(ProductVariant productVariant);
    Task UpdateAsync(ProductVariant productVariant);
    Task DeleteAsync(int id);
}
