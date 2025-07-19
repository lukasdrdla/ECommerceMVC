
using ECommerceMVC.Domain.Entities;

namespace ECommerceMVC.Web.Models.ViewModels
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; } = null!;
        public IEnumerable<Product> RelatedProducts { get; set; } = new List<Product>();
        public IEnumerable<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
        public double AverageRating { get; set; }
        public int ReviewsCount { get; set; }
        public bool IsInStock { get; set; }
        public int StockQuantity { get; set; }
        public ProductVariant? SelectedVariant { get; set; }
        public int SelectedQuantity { get; set; } = 1;
    }
}
