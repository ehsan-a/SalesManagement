namespace SalesManagement.Models.Entities
{
    public class TransactionProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int TransactionId { get; set; }
        public Transaction? Transaction { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
