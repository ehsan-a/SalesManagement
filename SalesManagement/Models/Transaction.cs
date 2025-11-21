namespace SalesManagement.Models
{
    public enum TranactionType
    {
        Buy,
        Sell,
    }
    public class Transaction
    {
        public int Id { get; set; }
        public TranactionType Type { get; set; }
        public DateTime DateTime { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public List<TransactionProduct>? TransactionProducts { get; set; }

    }
}
