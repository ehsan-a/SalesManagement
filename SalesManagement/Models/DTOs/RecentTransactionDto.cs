namespace SalesManagement.Models.DTOs
{
    public class RecentTransactionDto
    {
        public int TransactionId { get; set; }
        public string Type { get; set; } = "";
        public string ProductTitle { get; set; } = "";
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}
