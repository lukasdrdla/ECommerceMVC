using System.Threading.Tasks;
using ECommerceMVC.Application.Interfaces;
using ECommerceMVC.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMVC.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;

        public HomeController(
            IProductService productService, 
            ICategoryService categoryService, 
            IBrandService brandService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeIndexViewModel
            {
                // Získání featured produktů
                FeaturedProducts = await _productService.GetFeaturedProductsAsync(),
                
                // Získání nových produktů (posledních 30 dní)
                NewProducts = await _productService.GetNewProductsAsync(30),
                
                // Získání nejprodávanějších produktů
                BestSellingProducts = await _productService.GetBestSellingProductsAsync(),
                
                // Získání produktů ve slevě
                SaleProducts = await _productService.GetDiscountedProductsAsync(),
                
                // Získání kategorií
                Categories = await _categoryService.GetAllCategoriesAsync(),
                TopCategories = await _categoryService.GetTopCategoriesAsync(6),
                
                // Získání značek
                FeaturedBrands = await _brandService.GetFeaturedBrandsAsync(),
                
                // Statická data pro slider (v reálné aplikaci by byla z databáze)
                SliderItems = GetSliderItems()
            };

            return View(viewModel);
        }

        private IEnumerable<SliderItem> GetSliderItems()
        {
            return new List<SliderItem>
            {
                new SliderItem
                {
                    ImageUrl = "https://images.unsplash.com/photo-1560472354-b33ff0c44a43?ixlib=rb-4.0.3&auto=format&fit=crop&w=1920&q=80",
                    Title = "Nejnovější iPhone 15 Pro",
                    Subtitle = "Sleva až 20% na všechny modely",
                    ButtonText = "Koupit nyní",
                    ButtonUrl = "/Product",
                    IsActive = true
                },
                new SliderItem
                {
                    ImageUrl = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?ixlib=rb-4.0.3&auto=format&fit=crop&w=1920&q=80",
                    Title = "Sportovní kolekce Nike",
                    Subtitle = "Nové modely běžeckých bot",
                    ButtonText = "Zobrazit kolekci",
                    ButtonUrl = "/Product",
                    IsActive = false
                },
                new SliderItem
                {
                    ImageUrl = "https://images.unsplash.com/photo-1498049794561-7780e7231661?ixlib=rb-4.0.3&auto=format&fit=crop&w=1920&q=80",
                    Title = "Gaming Setup",
                    Subtitle = "Sestavte si perfektní herní sestavu",
                    ButtonText = "Gaming sekce",
                    ButtonUrl = "/Product",
                    IsActive = false
                }
            };
        }
    }
}