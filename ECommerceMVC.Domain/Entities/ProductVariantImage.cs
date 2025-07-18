namespace ECommerceMVC.Domain.Entities;

public class ProductVariantImage
{
    public int Id { get; set; }

    public int ProductVariantId { get; set; }
    public ProductVariant ProductVariant { get; set; } 

    public string ImageUrl { get; set; } = string.Empty;
    public bool IsMain { get; set; } = false;
}