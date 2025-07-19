using ECommerceMVC.Domain.Entities;

namespace ECommerceMVC.Domain.Interfaces;

public interface IProductVariantRepository
{
    Task<ProductVariant?> GetByIdAsync(int id);
    Task<IEnumerable<ProductVariant>> GetAllAsync();
    Task<ProductVariant> AddAsync(ProductVariant productVariant);
    Task<ProductVariant> UpdateAsync(ProductVariant productVariant);
    Task DeleteAsync(int id);
    Task<IEnumerable<ProductVariant>> GetVariantsByProductIdAsync(int productId);
}
