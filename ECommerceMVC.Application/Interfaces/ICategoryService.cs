using ECommerceMVC.Domain.Entities;

namespace ECommerceMVC.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<IEnumerable<Category>> GetTopCategoriesAsync(int count = 6);
        Task<IEnumerable<Category>> GetParentCategoriesAsync();
        Task<IEnumerable<Category>> GetSubCategoriesAsync(int parentId);
        Task<int> GetProductsCountByCategoryAsync(int categoryId);
    }
}
