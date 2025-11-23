namespace SalesManagement.Models.Entities
{
    public enum TransactionType
    {
        Buy,
        Sell,
    }
    public class Transaction
    {
        public int Id { get; set; }
        public TransactionType Type { get; set; }
        public DateTime DateTime { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public List<TransactionProduct>? TransactionProducts { get; set; }

    }
}
