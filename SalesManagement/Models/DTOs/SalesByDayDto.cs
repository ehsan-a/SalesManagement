namespace SalesManagement.Models.DTOs
{
    public class SalesByDayDto
    {
        public DateTime DateTime { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
