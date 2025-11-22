using SalesManagement.Models.Entities;

namespace SalesManagement.Models.DTOs
{
    public class MovementsDto
    {
        public DateTime DateTime { get; set; }
        public TranactionType TranactionType { get; set; }
        public int Quantity { get; set; }
    }
}
