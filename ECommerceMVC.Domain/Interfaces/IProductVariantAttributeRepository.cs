using ECommerceMVC.Domain.Entities;

namespace ECommerceMVC.Domain.Interfaces;

public interface IProductVariantAttributeRepository
{
    Task<ProductVariantAttribute?> GetByIdAsync(int id);
    Task<IEnumerable<ProductVariantAttribute>> GetAllAsync();
    Task AddAsync(ProductVariantAttribute productVariantAttribute);
    Task UpdateAsync(ProductVariantAttribute productVariantAttribute);
    Task DeleteAsync(int id);
}
