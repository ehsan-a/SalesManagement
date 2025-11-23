namespace SalesManagement.Models.DTOs
{
    public class LowStockItemDto
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; } = "";
        public string CategoryTitle { get; set; } = "";
        public int Stock { get; set; }
        public int MinStock { get; set; }
    }
}
