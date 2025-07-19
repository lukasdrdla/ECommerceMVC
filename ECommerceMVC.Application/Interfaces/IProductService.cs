using ECommerceMVC.Domain.Entities;

namespace ECommerceMVC.Application.Interfaces
{
    public interface IProductService
    {
        // Základní CRUD operace
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<bool> ProductExistsAsync(int id);

        // Filtrování a vyhledávání
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<IEnumerable<Product>> GetProductsByBrandAsync(int brandId);
        Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm);
        Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice);

        // Speciální kolekce
        Task<IEnumerable<Product>> GetFeaturedProductsAsync();
        Task<IEnumerable<Product>> GetNewProductsAsync(int days = 30);
        Task<IEnumerable<Product>> GetBestSellingProductsAsync();
        Task<IEnumerable<Product>> GetDiscountedProductsAsync();
        Task<IEnumerable<Product>> GetRelatedProductsAsync(int productId, int count = 4);

        // Paginace a řazení
        Task<(IEnumerable<Product> Products, int TotalCount)> GetProductsPagedAsync(
            int page = 1, 
            int pageSize = 12, 
            int? categoryId = null, 
            int? brandId = null, 
            string? searchTerm = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            string sortBy = "name",
            bool ascending = true);

        // Statistiky a metadata
        Task<int> GetProductsCountAsync();
        Task<int> GetProductsCountByCategoryAsync(int categoryId);
        Task<decimal> GetMinPriceAsync();
        Task<decimal> GetMaxPriceAsync();
        Task<(decimal Min, decimal Max)> GetPriceRangeAsync();

        // Inventory a dostupnost
        Task<bool> IsProductInStockAsync(int productId);
        Task<int> GetProductStockQuantityAsync(int productId);
        Task<IEnumerable<Product>> GetOutOfStockProductsAsync();
        Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold = 10);

        // Reviews a rating
        Task<double> GetProductAverageRatingAsync(int productId);
        Task<int> GetProductReviewsCountAsync(int productId);
        Task<IEnumerable<Product>> GetTopRatedProductsAsync(int count = 10);

        // Kategorie a tagy
        Task<IEnumerable<Product>> GetProductsByTagAsync(int tagId);
        Task<IEnumerable<Product>> GetProductsByMultipleTagsAsync(IEnumerable<int> tagIds);
        Task<IEnumerable<Product>> GetSimilarProductsAsync(int productId, int count = 4);

        // Admin a management
        Task<IEnumerable<Product>> GetInactiveProductsAsync();
        Task<Product> ToggleProductActiveStatusAsync(int productId);
        Task<Product> ToggleFeaturedStatusAsync(int productId);
        Task BulkUpdatePricesAsync(IEnumerable<int> productIds, decimal percentage, bool isIncrease = true);
        Task BulkUpdateCategoryAsync(IEnumerable<int> productIds, int newCategoryId);
    }
}
