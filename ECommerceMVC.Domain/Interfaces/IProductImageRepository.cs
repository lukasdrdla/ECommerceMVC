using ECommerceMVC.Domain.Entities;

namespace ECommerceMVC.Domain.Interfaces;

public interface IProductImageRepository
{
    Task<ProductImage?> GetByIdAsync(int id);
    Task<IEnumerable<ProductImage>> GetAllAsync();
    Task AddAsync(ProductImage productImage);
    Task UpdateAsync(ProductImage productImage);
    Task DeleteAsync(int id);
}
