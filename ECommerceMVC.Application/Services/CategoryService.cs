using ECommerceMVC.Application.Interfaces;
using ECommerceMVC.Domain.Entities;
using ECommerceMVC.Domain.Interfaces;

namespace ECommerceMVC.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public CategoryService(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Category>> GetTopCategoriesAsync(int count = 6)
        {
            var allCategories = await _categoryRepository.GetAllAsync();
            return allCategories.Take(count);
        }

        public async Task<IEnumerable<Category>> GetParentCategoriesAsync()
        {
            var allCategories = await _categoryRepository.GetAllAsync();
            return allCategories; // Všechny kategorie - hierarchie zatím není implementována
        }

        public async Task<IEnumerable<Category>> GetSubCategoriesAsync(int parentId)
        {
            var allCategories = await _categoryRepository.GetAllAsync();
            return allCategories; // Zatím vracíme všechny kategorie
        }

        public async Task<int> GetProductsCountByCategoryAsync(int categoryId)
        {
            var products = await _productRepository.GetAllAsync();
            return products.Count(p => p.CategoryId == categoryId && p.IsActive);
        }
    }
}
