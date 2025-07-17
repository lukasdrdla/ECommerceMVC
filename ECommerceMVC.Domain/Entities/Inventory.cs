namespace ECommerceMVC.Domain.Entities;

public class Inventory
{
    public int Id { get; set; }

    public int ProductVariantId { get; set; }
    public ProductVariant ProductVariant { get; set; } = new ProductVariant();

    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } = new Warehouse();

    public int Quantity { get; set; }
}