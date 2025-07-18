using ECommerceMVC.Domain.Entities;

namespace ECommerceMVC.Domain.Interfaces;

public interface IInventoryRepository
{
    Task<Inventory?> GetByIdAsync(int id);
    Task<IEnumerable<Inventory>> GetAllAsync();
    Task AddAsync(Inventory inventory);
    Task UpdateAsync(Inventory inventory);
    Task DeleteAsync(int id);
    
    Task<IEnumerable<Inventory>> GetByWarehouseIdAsync(int warehouseId);
    Task<IEnumerable<Inventory>> GetByProductVariantIdAsync(int productVariantId);
    Task<Inventory?> GetByProductVariantAndWarehouseAsync(int productVariantId, int warehouseId);
    Task<IEnumerable<Warehouse>> GetWarehousesWithProductVariantAsync(int productVariantId);

    
}