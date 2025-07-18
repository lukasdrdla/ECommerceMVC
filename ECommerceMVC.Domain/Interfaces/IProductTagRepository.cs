using ECommerceMVC.Domain.Entities;

namespace ECommerceMVC.Domain.Interfaces;

public interface IProductTagRepository
{
    Task<IEnumerable<ProductTag>> GetByProductIdAsync(int productId);
    Task AddAsync(ProductTag productTag);
    Task DeleteAsync(int productId, int tagId);
}