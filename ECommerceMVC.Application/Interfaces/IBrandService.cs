using ECommerceMVC.Domain.Entities;

namespace ECommerceMVC.Application.Interfaces
{
    public interface IBrandService
    {
        Task<IEnumerable<Brand>> GetAllBrandsAsync();
        Task<Brand?> GetBrandByIdAsync(int id);
        Task<IEnumerable<Brand>> GetFeaturedBrandsAsync();
        Task<IEnumerable<Brand>> GetTopBrandsAsync(int count = 8);
        Task<int> GetProductsCountByBrandAsync(int brandId);
    }
}
