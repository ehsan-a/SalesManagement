using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagement.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int TypeId { get; set; }
        public bool IsActive { get; set; }
        public int MinQuantity { get; set; }
        public ProductType? Type { get; set; }
        public List<TransactionProduct>? TransactionProducts { get; set; }
    }
}
