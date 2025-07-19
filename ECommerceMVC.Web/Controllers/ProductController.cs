using Microsoft.AspNetCore.Mvc;
using ECommerceMVC.Application.Interfaces;
using ECommerceMVC.Web.Models.ViewModels;

namespace ECommerceMVC.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductVariantService _productVariantService;

        public ProductController(IProductService productService, IProductVariantService productVariantService)
        {
            _productService = productService;
            _productVariantService = productVariantService;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            
            var viewModel = new ProductIndexViewModel
            {
                Products = products,
                Categories = new List<ECommerceMVC.Domain.Entities.Category>(), // Zatím prázdné
                CurrentPage = 1,
                TotalPages = 1,
                SelectedCategoryId = null
            };
            
            return View(viewModel);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            // Získání souvisejících produktů
            var relatedProducts = await _productService.GetRelatedProductsAsync(id);
            
            var viewModel = new ProductDetailViewModel
            {
                Product = product,
                RelatedProducts = relatedProducts,
                AverageRating = await _productService.GetProductAverageRatingAsync(id),
                ReviewsCount = await _productService.GetProductReviewsCountAsync(id),
                IsInStock = await _productService.IsProductInStockAsync(id),
                StockQuantity = await _productService.GetProductStockQuantityAsync(id),
                ProductVariants = await _productVariantService.GetProductVariantsByProductIdAsync(id),
            };

            return View(viewModel);
        }
    }
}
