namespace ECommerceMVC.Domain.Entities;

public class ProductImage
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; } = new Product();

    public string ImageUrl { get; set; } = string.Empty;

    public bool IsMain { get; set; } = false; // zda je hlavní obrázek
    public int DisplayOrder { get; set; } = 0;
}