using ECommerceMVC.Domain.Entities;

namespace ECommerceMVC.Web.Models.ViewModels
{
    public class ProductIndexViewModel
    {
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public int? SelectedCategoryId { get; set; }
    }


}
