using ECommerceMVC.Application.Interfaces;
using ECommerceMVC.Domain.Entities;
using ECommerceMVC.Domain.Interfaces;

namespace ECommerceMVC.Application.Services
{


    public class ProductVariantService : IProductVariantService
    {
        private readonly IProductVariantRepository _productVariantRepository;

        public ProductVariantService(IProductVariantRepository productVariantRepository)
        {
            _productVariantRepository = productVariantRepository;
        }

        public Task<IEnumerable<ProductVariant>> GetAllProductVariantsAsync()
        {
            return _productVariantRepository.GetAllAsync();
        }

        public Task<ProductVariant> GetProductVariantByIdAsync(int id)
        {
            return _productVariantRepository.GetByIdAsync(id);
        }

        public Task<IEnumerable<ProductVariant>> GetProductVariantsByProductIdAsync(int productId)
        {
            var variants = _productVariantRepository.GetAllAsync()
                .Result.Where(v => v.ProductId == productId);
            return Task.FromResult(variants);
        }
    }
}