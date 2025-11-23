using SalesManagement.Models.DTOs;
using SalesManagement.Models.Entities;

namespace SalesManagement.Services.Interfaces
{
    public interface IProductService : IService<Product>
    {
        Task<IEnumerable<StockDto>> GetItemsAsync();
        IEnumerable<StockDto> Filter(IEnumerable<StockDto> items, string searchString, string productType, string productCategory);
        Task<int> GetTotalStockQuantityAsync();
        Task<int> GetActiveCountAsync();
        Task<List<LowStockItemDto>> GetLowStockItemsAsync();
    }
}
