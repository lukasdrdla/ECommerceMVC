using ECommerceMVC.Domain.Entities;

namespace ECommerceMVC.Web.Models.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Product> FeaturedProducts { get; set; } = new List<Product>();
        public IEnumerable<Product> NewProducts { get; set; } = new List<Product>();
        public IEnumerable<Product> BestSellingProducts { get; set; } = new List<Product>();
        public IEnumerable<Product> SaleProducts { get; set; } = new List<Product>();
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public IEnumerable<Category> TopCategories { get; set; } = new List<Category>();
        public IEnumerable<Brand> FeaturedBrands { get; set; } = new List<Brand>();
        
        // Slider data
        public IEnumerable<SliderItem> SliderItems { get; set; } = new List<SliderItem>();
    }

    public class SliderItem
    {
        public string ImageUrl { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        public string ButtonText { get; set; } = string.Empty;
        public string ButtonUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
    }
}
