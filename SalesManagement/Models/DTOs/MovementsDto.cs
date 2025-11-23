using SalesManagement.Models.Entities;

namespace SalesManagement.Models.DTOs
{
    public class MovementsDto
    {
        public DateTime DateTime { get; set; }
        public TransactionType TransactionType { get; set; }
        public int Quantity { get; set; }
    }
}
