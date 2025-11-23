using SalesManagement.Models.Entities;

namespace SalesManagement.Models.DTOs
{
    public class StockDto
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
