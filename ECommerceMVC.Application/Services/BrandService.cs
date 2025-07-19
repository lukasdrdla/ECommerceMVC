using ECommerceMVC.Application.Interfaces;
using ECommerceMVC.Domain.Entities;
using ECommerceMVC.Domain.Interfaces;

namespace ECommerceMVC.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IProductRepository _productRepository;

        public BrandService(IBrandRepository brandRepository, IProductRepository productRepository)
        {
            _brandRepository = brandRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            return await _brandRepository.GetAllAsync();
        }

        public async Task<Brand?> GetBrandByIdAsync(int id)
        {
            return await _brandRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Brand>> GetFeaturedBrandsAsync()
        {
            var allBrands = await _brandRepository.GetAllAsync();
            return allBrands.Take(8); // Top 8 značek
        }

        public async Task<IEnumerable<Brand>> GetTopBrandsAsync(int count = 8)
        {
            var allBrands = await _brandRepository.GetAllAsync();
            return allBrands.Take(count);
        }

        public async Task<int> GetProductsCountByBrandAsync(int brandId)
        {
            var products = await _productRepository.GetAllAsync();
            return products.Count(p => p.BrandId == brandId && p.IsActive);
        }
    }
}
