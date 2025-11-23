using SalesManagement.Models.DTOs;
namespace SalesManagement.Models.ViewModels
{
    public class DashboardViewModel
    {

        public int TotalStockQuantity { get; set; }
        public int CategoriesCount { get; set; }
        public int ActiveProductsCount { get; set; }
        public decimal SalesThisMonth { get; set; }
        public List<string> SalesLast30Labels { get; set; } = new();
        public List<decimal> SalesLast30Values { get; set; } = new();
        public List<string> MovementLabels { get; set; } = new();
        public List<int> MovementBuy { get; set; } = new();
        public List<int> MovementSell { get; set; } = new();
        public List<string> CategoryLabels { get; set; } = new();
        public List<int> CategoryStockShare { get; set; } = new();
        public List<LowStockItemDto> LowStockItems { get; set; } = new();
        public List<RecentTransactionDto> RecentTransactions { get; set; } = new();
    }

}
