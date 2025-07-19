using ECommerceMVC.Domain.Entities;

namespace ECommerceMVC.Application.Interfaces
{
    public interface IProductVariantService
    {
        Task<ProductVariant> GetProductVariantByIdAsync(int id);
        Task<IEnumerable<ProductVariant>> GetAllProductVariantsAsync();
        Task<IEnumerable<ProductVariant>> GetProductVariantsByProductIdAsync(int productId);
        
    }
}