using ECommerceMVC.Domain.Entities;

namespace ECommerceMVC.Domain.Interfaces;

public interface IProductVariantImageRepository
{
    Task<ProductVariantImage?> GetByIdAsync(int id);
    Task<IEnumerable<ProductVariantImage>> GetAllAsync();
    Task AddAsync(ProductVariantImage productVariantImage);
    Task UpdateAsync(ProductVariantImage productVariantImage);
    Task DeleteAsync(int id);
}
