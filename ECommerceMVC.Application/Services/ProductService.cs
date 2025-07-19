using ECommerceMVC.Application.Interfaces;
using ECommerceMVC.Domain.Entities;
using ECommerceMVC.Domain.Interfaces;

namespace ECommerceMVC.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;

        public ProductService(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IBrandRepository brandRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
        }

        // Základní CRUD operace
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;
            await _productRepository.AddAsync(product);
            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            product.UpdatedAt = DateTime.UtcNow;
            await _productRepository.UpdateAsync(product);
            return product;
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task<bool> ProductExistsAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product != null;
        }

        // Filtrování a vyhledávání
        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await _productRepository.GetAllAsync();
            return products.Where(p => p.CategoryId == categoryId && p.IsActive);
        }

        public async Task<IEnumerable<Product>> GetProductsByBrandAsync(int brandId)
        {
            var products = await _productRepository.GetAllAsync();
            return products.Where(p => p.BrandId == brandId && p.IsActive);
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return Enumerable.Empty<Product>();

            var products = await _productRepository.GetAllAsync();
            return products.Where(p => 
                p.IsActive && (
                    p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    p.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                ));
        }

        public async Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var products = await _productRepository.GetAllAsync();
            return products.Where(p => p.IsActive && 
                p.Variants.Any(v => v.Price >= minPrice && v.Price <= maxPrice));
        }

        // Speciální kolekce
        public async Task<IEnumerable<Product>> GetFeaturedProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Where(p => p.IsHomePageFeatured && p.IsActive).Take(8);
        }

        public async Task<IEnumerable<Product>> GetNewProductsAsync(int days = 30)
        {
            var cutoffDate = DateTime.UtcNow.AddDays(-days);
            var products = await _productRepository.GetAllAsync();
            return products.Where(p => p.IsActive && p.CreatedAt >= cutoffDate)
                          .OrderByDescending(p => p.CreatedAt)
                          .Take(8);
        }

        public async Task<IEnumerable<Product>> GetBestSellingProductsAsync()
        {
            // Zatím mockovaná implementace - později přidat logiku na základě objednávek
            var products = await _productRepository.GetAllAsync();
            return products.Where(p => p.IsActive)
                          .OrderByDescending(p => p.Id) // Placeholder
                          .Take(8);
        }

        public async Task<IEnumerable<Product>> GetDiscountedProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Where(p => p.IsActive && 
                p.Variants.Any(v => v.DiscountedPrice < v.Price));
        }

        public async Task<IEnumerable<Product>> GetRelatedProductsAsync(int productId, int count = 4)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null) return Enumerable.Empty<Product>();

            var products = await _productRepository.GetAllAsync();
            return products.Where(p => p.Id != productId && 
                                 p.CategoryId == product.CategoryId && 
                                 p.IsActive)
                          .Take(count);
        }

        // Paginace a řazení
        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetProductsPagedAsync(
            int page = 1, 
            int pageSize = 12, 
            int? categoryId = null, 
            int? brandId = null, 
            string? searchTerm = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            string sortBy = "name",
            bool ascending = true)
        {
            var products = await _productRepository.GetAllAsync();
            var query = products.Where(p => p.IsActive).AsQueryable();

            // Aplikace filtrů
            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value);

            if (brandId.HasValue)
                query = query.Where(p => p.BrandId == brandId.Value);

            if (!string.IsNullOrWhiteSpace(searchTerm))
                query = query.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                       p.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

            if (minPrice.HasValue || maxPrice.HasValue)
            {
                query = query.Where(p => p.Variants.Any(v => 
                    (!minPrice.HasValue || v.Price >= minPrice.Value) &&
                    (!maxPrice.HasValue || v.Price <= maxPrice.Value)));
            }

            // Řazení
            query = sortBy.ToLower() switch
            {
                "name" => ascending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name),
                "created" => ascending ? query.OrderBy(p => p.CreatedAt) : query.OrderByDescending(p => p.CreatedAt),
                "price" => ascending ? 
                    query.OrderBy(p => p.Variants.Any() ? p.Variants.Min(v => v.Price) : 0) : 
                    query.OrderByDescending(p => p.Variants.Any() ? p.Variants.Max(v => v.Price) : 0),
                _ => query.OrderBy(p => p.Name)
            };

            var totalCount = query.Count();
            var pagedProducts = query.Skip((page - 1) * pageSize).Take(pageSize);

            return (pagedProducts, totalCount);
        }

        // Statistiky a metadata
        public async Task<int> GetProductsCountAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Count(p => p.IsActive);
        }

        public async Task<int> GetProductsCountByCategoryAsync(int categoryId)
        {
            var products = await _productRepository.GetAllAsync();
            return products.Count(p => p.CategoryId == categoryId && p.IsActive);
        }

        public async Task<decimal> GetMinPriceAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var activeProductsWithVariants = products.Where(p => p.IsActive && p.Variants.Any());
            return activeProductsWithVariants.Any() ? 
                activeProductsWithVariants.SelectMany(p => p.Variants).Min(v => v.Price) : 0;
        }

        public async Task<decimal> GetMaxPriceAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var activeProductsWithVariants = products.Where(p => p.IsActive && p.Variants.Any());
            return activeProductsWithVariants.Any() ? 
                activeProductsWithVariants.SelectMany(p => p.Variants).Max(v => v.Price) : 0;
        }

        public async Task<(decimal Min, decimal Max)> GetPriceRangeAsync()
        {
            var min = await GetMinPriceAsync();
            var max = await GetMaxPriceAsync();
            return (min, max);
        }

        // Inventory a dostupnost
        public async Task<bool> IsProductInStockAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            return product?.Variants.Any(v => v.StockQuantity > 0) ?? false;
        }

        public async Task<int> GetProductStockQuantityAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            return product?.Variants.Sum(v => v.StockQuantity) ?? 0;
        }

        public async Task<IEnumerable<Product>> GetOutOfStockProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Where(p => p.IsActive && 
                (!p.Variants.Any() || p.Variants.All(v => v.StockQuantity <= 0)));
        }

        public async Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold = 10)
        {
            var products = await _productRepository.GetAllAsync();
            return products.Where(p => p.IsActive && 
                p.Variants.Any() && 
                p.Variants.Sum(v => v.StockQuantity) <= threshold);
        }

        // Reviews a rating
        public async Task<double> GetProductAverageRatingAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            return product?.AverageRating ?? 0;
        }

        public async Task<int> GetProductReviewsCountAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            return product?.Reviews.Count ?? 0;
        }

        public async Task<IEnumerable<Product>> GetTopRatedProductsAsync(int count = 10)
        {
            var products = await _productRepository.GetAllAsync();
            return products.Where(p => p.IsActive && p.Reviews.Any())
                          .OrderByDescending(p => p.AverageRating)
                          .Take(count);
        }

        // Kategorie a tagy
        public async Task<IEnumerable<Product>> GetProductsByTagAsync(int tagId)
        {
            var products = await _productRepository.GetAllAsync();
            return products.Where(p => p.IsActive && 
                p.ProductTags.Any(pt => pt.TagId == tagId));
        }

        public async Task<IEnumerable<Product>> GetProductsByMultipleTagsAsync(IEnumerable<int> tagIds)
        {
            var products = await _productRepository.GetAllAsync();
            return products.Where(p => p.IsActive && 
                tagIds.All(tagId => p.ProductTags.Any(pt => pt.TagId == tagId)));
        }

        public async Task<IEnumerable<Product>> GetSimilarProductsAsync(int productId, int count = 4)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null) return Enumerable.Empty<Product>();

            var products = await _productRepository.GetAllAsync();
            
            // Najdi produkty se společnými tagy
            var productTags = product.ProductTags.Select(pt => pt.TagId).ToList();
            var similarProducts = products.Where(p => p.Id != productId && 
                                                p.IsActive &&
                                                p.ProductTags.Any(pt => productTags.Contains(pt.TagId)))
                                           .OrderByDescending(p => p.ProductTags.Count(pt => productTags.Contains(pt.TagId)))
                                           .Take(count);

            // Pokud není dost produktů s tagy, doplň podle kategorie
            if (similarProducts.Count() < count)
            {
                var categoryProducts = await GetRelatedProductsAsync(productId, count - similarProducts.Count());
                similarProducts = similarProducts.Concat(categoryProducts).Take(count);
            }

            return similarProducts;
        }

        // Admin a management
        public async Task<IEnumerable<Product>> GetInactiveProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Where(p => !p.IsActive);
        }

        public async Task<Product> ToggleProductActiveStatusAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null) throw new ArgumentException("Product not found");

            product.IsActive = !product.IsActive;
            product.UpdatedAt = DateTime.UtcNow;
            
            return await _productRepository.UpdateAsync(product);
        }

        public async Task<Product> ToggleFeaturedStatusAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null) throw new ArgumentException("Product not found");

            product.IsHomePageFeatured = !product.IsHomePageFeatured;
            product.UpdatedAt = DateTime.UtcNow;
            
            return await _productRepository.UpdateAsync(product);
        }

        public async Task BulkUpdatePricesAsync(IEnumerable<int> productIds, decimal percentage, bool isIncrease = true)
        {
            foreach (var productId in productIds)
            {
                var product = await _productRepository.GetByIdAsync(productId);
                if (product != null)
                {
                    foreach (var variant in product.Variants)
                    {
                        var multiplier = isIncrease ? (1 + percentage / 100) : (1 - percentage / 100);
                        variant.Price = Math.Round(variant.Price * multiplier, 2);
                    }
                    
                    product.UpdatedAt = DateTime.UtcNow;
                    await _productRepository.UpdateAsync(product);
                }
            }
        }

        public async Task BulkUpdateCategoryAsync(IEnumerable<int> productIds, int newCategoryId)
        {
            foreach (var productId in productIds)
            {
                var product = await _productRepository.GetByIdAsync(productId);
                if (product != null)
                {
                    product.CategoryId = newCategoryId;
                    product.UpdatedAt = DateTime.UtcNow;
                    await _productRepository.UpdateAsync(product);
                }
            }
        }
    }
}

